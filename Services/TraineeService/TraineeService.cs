using Microsoft.EntityFrameworkCore;
using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;
using trainee_management.Validator;
using trainee_management.Exceptions;
using trainee_management.Database;
using trainee_management.Enums;
namespace trainee_management.Services
{

    public class TraineeService : ItraineeService
    {
        private readonly AppDBContext _context;
        public TraineeService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Trainee?> getTraineeById(int id)
        {
            Trainee trainee = await _context.Trainee.FindAsync(id) 
            ?? throw new NotFoundException("Trainee Not found");
            return trainee;
        }


        public async Task<GetAllDTO<TraineeResponse>> returnTrainees(string searchParams,string status ,int pageNumber, int pageSize)
        {
           IQueryable<Trainee> trainees=_context.Trainee;
            trainees=await filterBySearch(searchParams,status,trainees);
            trainees=getPaginatedData(pageNumber,pageSize,trainees);
            List<TraineeResponse> traineesList = await trainees.Select(t => new TraineeResponse
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
                TechStack = t.TechStack,
                CreatedDate = t.CreatedDate,
                Status = t.Status
            }).ToListAsync();

            GetAllDTO<TraineeResponse> response=new GetAllDTO<TraineeResponse>
            {
                pageNumber=pageNumber,
                pageSize=pageSize,
                totalCount=traineesList.Count,
                data=traineesList
            };
            return response;
        }

      


        // filter By Search takes the paginated Data and filters by search params provided
        public async Task<IQueryable<Trainee>> filterBySearch(string searchParams,string status,IQueryable<Trainee> trainees)
        {
            if (!string.IsNullOrWhiteSpace(searchParams))
            {
                searchParams=searchParams.Trim().ToLower();
                trainees=trainees.Where(t=>
                    t.FirstName.ToLower().Contains(searchParams)||
                    t.LastName.ToLower().Contains(searchParams)||
                    t.Email.ToLower().Contains(searchParams)||
                    t.TechStack.ToLower().Contains(searchParams)
                );
            }

            if (!string.IsNullOrWhiteSpace(status) &&  Enum.TryParse(status,true,out StatusValues result))
            {
                trainees=trainees.Where(t=>t.Status==result);
            }
            return trainees; 
        }

        //pagination
        public IQueryable<Trainee> getPaginatedData(int pageNumber,int pageSize,IQueryable<Trainee> trainees)
        {
            trainees=trainees.Skip((pageNumber-1)*pageSize).Take(pageSize);
            return trainees;
        }

     

        public async Task checkIfTraineeExists(string email)
        {
            bool exists = await _context.Trainee.AnyAsync(t => t.Email == email);
            if (exists)throw new DuplicateEmailException("User with this email already exists");
        }
        public async Task createTrainee(CreateTraineeRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);
            Trainee trainee = new Trainee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email.Trim().ToLower(),
                Status = request.Status,
                CreatedDate = DateTime.Today,
                UpdatedDate = DateTime.Today,
                TechStack = request.TechStack,

            };

            TraineeValidator validator = new TraineeValidator(trainee);
            if (!validator.Validate())
            {
                throw new ValidationException("Invalid Input");
            }
            await _context.Trainee.AddAsync(trainee);
            await _context.SaveChangesAsync();
        }

        public TraineeResponse getTraineeResponse(Trainee trainee)
        {
            TraineeResponse response = new TraineeResponse
            {
                Id = trainee.Id,
                FirstName = trainee.FirstName,
                LastName = trainee.LastName,
                Email = trainee.Email,
                TechStack = trainee.TechStack,
                CreatedDate = trainee.CreatedDate,
                Status = trainee.Status
            };
            return response;
        }

        public async Task updateTrainee(UpdateTraineeRequest request, Trainee trainee)
        {
                trainee.FirstName = request.FirstName;
                trainee.LastName = request.LastName;
                trainee.Email = request.Email;
                trainee.TechStack = request.TechStack;
                trainee.Status = request.Status;
                trainee.UpdatedDate = DateTime.Today;
                await _context.SaveChangesAsync();
        }

        public async Task<bool> deleteTrainee(Trainee trainee)
        {
            try
            {
                _context.Remove(trainee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
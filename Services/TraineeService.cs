using Microsoft.EntityFrameworkCore;
using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;
using trainee_management.Validator;
using trainee_management.Exceptions;
using trainee_management.Database;
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
            Trainee? trainee = await _context.Trainee.FindAsync(id);
            return trainee;
        }


        public async Task<List<TraineeResponse>> returnTrainees(string searchParams, int pageNumber, int pageSize)
        {
           IQueryable<Trainee> trainees=_context.Trainee;
            trainees=filterBySearch(searchParams,trainees);
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
            return traineesList;
        }

      


        // filter By Search takes the paginated Data and filters by search params provided
        public IQueryable<Trainee> filterBySearch(string searchParams,IQueryable<Trainee> trainees)
        {
            if (!string.IsNullOrWhiteSpace(searchParams))
            {
                searchParams=searchParams.Trim().ToLower();
                trainees=trainees.Where(t=>
                    t.FirstName.ToLower().Contains(searchParams)||
                    t.LastName.ToLower().Contains(searchParams)||
                    t.Email.ToLower().Contains(searchParams)
                );
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
            Trainee? trainee = await _context.Trainee.FirstOrDefaultAsync(t => t.Email == email);
            if (trainee != null)
            {
                throw new DuplicateEmailException("User with this email already exists");
            }
        }
        public async Task createTrainee(CreateTraineeRequest request)
        {
            if (request.Email == null)
            {
                throw new ValidationException("Email not provided");
            }

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

        public async Task<bool> updateTrainee(UpdateTraineeRequest request, Trainee trainee)
        {
            try
            {

                trainee.FirstName = request.FirstName;
                trainee.LastName = request.LastName;
                trainee.Email = request.Email;
                trainee.TechStack = request.TechStack;
                trainee.Status = request.Status;
                trainee.UpdatedDate = DateTime.Today;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

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
using Microsoft.EntityFrameworkCore;
using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;
using trainee_management.Validator;
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


        public async Task<List<TraineeResponse>> returnTrainees(string searchParams)
        {
            List<TraineeResponse> traineesList = await _context.Trainee.Select(t => new TraineeResponse
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
                TechStack = t.TechStack,
                CreatedDate = t.CreatedDate,
                Status = t.Status
            }).ToListAsync();

            if (!string.IsNullOrWhiteSpace(searchParams))
            {
                List<TraineeResponse> filteredTrainees =traineesList.Where(
                   t =>
                    t.FirstName.ToLower().Contains(searchParams.ToLower()) ||
                    t.LastName.ToLower().Contains(searchParams.ToLower()) ||
                    t.Email.ToLower().Contains(searchParams.ToLower()) ||
                    t.TechStack.ToLower().Contains(searchParams.ToLower())
                ).ToList();

                return filteredTrainees;
            }
            return traineesList;
        }

        public async Task<bool> traineeAlreadyExists(string email)
        {
            Trainee? trainee = await _context.Trainee.FirstOrDefaultAsync(t => t.Email == email);
            if (trainee == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> createTrainee(CreateTraineeRequest request)
        {
            if (request.Email == null)
            {
                Console.WriteLine("Email not provided by the user");
                return false;
            }
            try
            {
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
                TraineeValidator validator=new TraineeValidator(trainee);
                if (!validator.Validate())
                {
                    return false;
                }
                await _context.Trainee.AddAsync(trainee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch 
            {
                return false;
            }
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
                trainee.FirstName = request.FirstName;
                trainee.LastName = request.LastName;
                trainee.Email = request.Email;
                trainee.TechStack = request.TechStack;
                trainee.Status = request.Status;
                trainee.UpdatedDate = DateTime.Today;
                await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteTrainee(Trainee trainee)
        {
            _context.Remove(trainee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;

namespace trainee_management.Services
{

    public class TraineeService : ItraineeService
    {
        public static List<Trainee> traineeList = [];

        public Trainee getTraineeById(int id)
        {
            Trainee? trainee = traineeList.Find(t => t.Id == id);
            return trainee;
        }


        public List<TraineeResponse> returnTrainees()
        {
            List<TraineeResponse> traineesList = traineeList.Select(t => new TraineeResponse
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName,
                Email = t.Email,
                TechStack = t.TechStack,
                CreatedDate = t.CreatedDate,
                Status = t.Status
            }).ToList();

            return traineesList;
        }

        public Trainee getTraineeByEmail(string email)
        {
            Trainee? trainee = traineeList.Find(t => t.Email == email);
            return trainee;
        }


        public TraineeResponse createTrainee(CreateTraineeRequest request)
        {

            Console.WriteLine(traineeList.Count());
            Trainee trainee = new Trainee
            {
                Id = traineeList.Count() + 1,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email.Trim(),
                Status = request.Status,
                CreatedDate = DateTime.Today,
                UpdatedDate = DateTime.Today,
                TechStack = request.TechStack,

            };
            Console.WriteLine("Trainee Id: ", trainee.Id);
            traineeList.Add(trainee);

            TraineeResponse response = getTraineeResponse(trainee);

            return response;
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


        public TraineeResponse updateTrainee(UpdateTraineeRequest request, Trainee trainee)
        {
            trainee.FirstName = request.FirstName;
            trainee.LastName = request.LastName;
            trainee.Email = request.Email;
            trainee.TechStack = request.TechStack;
            trainee.Status = request.Status;
            trainee.UpdatedDate = DateTime.Today;
            return getTraineeResponse(trainee);
        }


        public TraineeResponse deleteTrainee(Trainee trainee)
        {
            traineeList.Remove(trainee);
            return getTraineeResponse(trainee);
        }

      
    }
}
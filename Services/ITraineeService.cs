using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;

namespace trainee_management.Services
{


    public interface ItraineeService
    {
        Task<List<TraineeResponse>> returnTrainees(string searchTerm);
        Task<Trainee?> getTraineeById(int id);
        
        Task<bool> createTrainee(CreateTraineeRequest request);
        Task<bool> traineeAlreadyExists(string email);

        TraineeResponse getTraineeResponse(Trainee trainee);

        Task<bool> updateTrainee(UpdateTraineeRequest request,Trainee trainee);

        Task<bool> deleteTrainee(Trainee trainee);

      
    }
}
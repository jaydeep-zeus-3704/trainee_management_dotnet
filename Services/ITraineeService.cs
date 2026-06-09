using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;

namespace trainee_management.Services
{


    public interface ItraineeService
    {
        Task<List<TraineeResponse>> returnTrainees(string searchTerm);
        Task<Trainee?> getTraineeById(int id);
        
        Task createTrainee(CreateTraineeRequest request);
        Task checkIfTraineeExists(string email);

        TraineeResponse getTraineeResponse(Trainee trainee);

        Task<bool> updateTrainee(UpdateTraineeRequest request,Trainee trainee);

        Task<bool> deleteTrainee(Trainee trainee);

      
    }
}
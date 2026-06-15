using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;

namespace trainee_management.Services
{


    public interface ItraineeService
    {
        Task<GetAllDTO<TraineeResponse>> returnTrainees(string searchTerm,string status,int pageNumber,int pageSize);
        Task<Trainee?> getTraineeById(int id);
        
        Task createTrainee(CreateTraineeRequest request);
        Task checkIfTraineeExists(string email);

        TraineeResponse getTraineeResponse(Trainee trainee);

        Task updateTrainee(UpdateTraineeRequest request,Trainee trainee);

        Task<bool> deleteTrainee(Trainee trainee);

      
    }
}
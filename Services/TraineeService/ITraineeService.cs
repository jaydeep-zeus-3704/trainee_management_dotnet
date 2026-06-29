using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;

namespace trainee_management.Services
{


    public interface ItraineeService
    {
        Task<GetAllDTO<TraineeResponse>> ReturnTrainees(string searchTerm,string status,int pageNumber,int pageSize);
        Task<Trainee?> GetTraineeById(int id);
        
        Task CreateTrainee(CreateTraineeRequest request);
        Task CheckIfTraineeExists(string email);

        TraineeResponse GetTraineeResponse(Trainee trainee);

        Task UpdateTrainee(UpdateTraineeRequest request,int id);

        Task DeleteTrainee(Trainee trainee);

      
    }
}
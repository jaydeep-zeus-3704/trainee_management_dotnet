using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;

namespace trainee_management.Services
{


    public interface ItraineeService
    {
        List<TraineeResponse> returnTrainees();
        Trainee getTraineeById(int id);
        
        TraineeResponse createTrainee(CreateTraineeRequest request);
        Trainee getTraineeByEmail(string email);

        TraineeResponse getTraineeResponse(Trainee trainee);

        TraineeResponse updateTrainee(UpdateTraineeRequest request,Trainee trainee);

        TraineeResponse deleteTrainee(Trainee trainee);
    }
}
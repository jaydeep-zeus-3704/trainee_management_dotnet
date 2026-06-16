using trainee_management.Models;
using trainee_management.Models.DTOs;

namespace trainee_management.Services;
public interface ILearningTaskService
{
    
    public   Task createLearningTask(LearningTaskRequest request);

    public Task<LearningTaskResponse> getLearningTaskById(int id);

    public  Task deleteLearningTask(int id);

    public  Task updateTask(int id,LearningTaskRequest request);

      public  Task<GetAllDTO<LearningTaskResponse>> getLearningTasks(string searchParams, string status, int pageNumber, int pageSize);

}
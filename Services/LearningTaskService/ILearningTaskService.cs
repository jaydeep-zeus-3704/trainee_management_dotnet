using trainee_management.Models;
using trainee_management.Models.DTOs;

namespace trainee_management.Services;
public interface ILearningTaskService
{
    
    public   Task CreateLearningTask(LearningTaskRequest request);

    public Task<LearningTaskResponse> GetLearningTaskById(int id);

    public  Task DeleteLearningTask(int id);

    public  Task UpdateTask(int id,LearningTaskRequest request);

      public  Task<GetAllDTO<LearningTaskResponse>> GetLearningTasks(string searchParams, string status, int pageNumber, int pageSize);

}
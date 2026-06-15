
using trainee_management.Models.DTOs;

namespace trainee_management.Services;
public interface ITaskAssignmentService
{
    public  Task CreateTaskAssignment(TaskAssignmentRequest request);

    public  Task<List<TaskAssignmentResponse>> GetTaskAssignments();

    public Task<TaskAssignmentResponse> GetTaskAssignmentById(int id);

    public Task UpdateTaskAssignmentStatus(int id,TaskAssignmentUpdate request);

}
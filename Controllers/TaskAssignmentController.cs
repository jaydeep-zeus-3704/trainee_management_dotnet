

using Microsoft.AspNetCore.Mvc;
using trainee_management.Models.DTOs;
using trainee_management.Services;

[ApiController]
[Route("api/task_assignment")]
public class TaskAssignmentController : ControllerBase
{
    private readonly ITaskAssignmentService _task_assignment_service;
    private readonly ILogger<TaskAssignmentController> _logger;

    public TaskAssignmentController(ITaskAssignmentService task_assignment_service,ILogger<TaskAssignmentController> logger)
    {
        _task_assignment_service=task_assignment_service;
        _logger=logger;
    }


    [HttpPost("create")]
    public async  Task <IActionResult> CreateTaskAssignment(TaskAssignmentRequest? request)
    {
        ArgumentNullException.ThrowIfNull(request);
        await _task_assignment_service.CreateTaskAssignment(request);
        _logger.LogInformation($"\nStatus Code:201\nmessage: Task assignment created sucessfully\npath: post - api/task_assignment/create");
        return StatusCode(201,new {message="task assigned sucessfully !"});
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAssignedTasks()
    {
        List<TaskAssignmentResponse> assigned_tasks=await _task_assignment_service.GetTaskAssignments();
        _logger.LogInformation($"\nStatus Code:200\nmessage: Assigned Task List Fetched Sucessfully");
        return StatusCode(200,new {assigned_tasks,message="Assigned tasks fetched sucessfully"});
    }  

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAssignedTaskById(int id)
    {
        TaskAssignmentResponse assignment=await _task_assignment_service.GetTaskAssignmentById(id);
        _logger.LogInformation($"\nStatus Code:200\nmessage: Assigned Task with id {id} Fetched Sucessfully");
        return StatusCode(200,new {assignment,message=$"Assigned task with id {id} fetched sucessfully"});
    }

    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateAssignedTask(int id,TaskAssignmentUpdate updateRequest)
    {  
        await _task_assignment_service.UpdateTaskAssignmentStatus(id,updateRequest);
        _logger.LogInformation($"\nStatus Code:200\nmessage: Assigned Task with id {id} updated Sucessfully");
        return StatusCode(200,new {message=$"Updated task status with id {id} fetched sucessfully"});
    }
}
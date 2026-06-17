
using Microsoft.AspNetCore.Mvc;
using trainee_management.Models;
using trainee_management.Services;
using trainee_management.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/learning-tasks")]
public class LearningTaskController : ControllerBase
{
    private readonly ILogger<LearningTaskController> _logger;
    private readonly ILearningTaskService _learning_task_service;
    public LearningTaskController(ILearningTaskService learning_task_service, ILogger<LearningTaskController> logger)
    {
        _learning_task_service = learning_task_service;
        _logger = logger;
    }

    //create learning task
    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> createLearningTask(LearningTaskRequest request)
    {
        await _learning_task_service.createLearningTask(request);
        _logger.LogInformation($"\nStatus Code:201\nmessage: Learning Task created sucessfully\npath: post - api/learning-task");
        return StatusCode(201, new { message = "learning task created !" });
    }

    //get learning task by id
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> getLearningTaskById(int id)
    {
        LearningTaskResponse response = await _learning_task_service.getLearningTaskById(id);
        _logger.LogInformation($"\nStatus Code:200\nmessage: Learning Task returned sucessfully\npath: get - api/learning-task/{id}");
        return StatusCode(200, new { response, message = $"learning task with id {id} fetched sucessfully" });
    }

    //delete learning task
    [Authorize]
    [HttpDelete("{id:int}/delete")]
    public async Task<IActionResult> deleteLearningTask(int id)
    {
        await _learning_task_service.deleteLearningTask(id);
        _logger.LogInformation($"\nStatus Code:204\nmessage: Learning Task deleted sucessfully\npath: delete - api/learning-task/{id}");
        return StatusCode(204);
    }

    //update learning task
    [Authorize]
    [HttpPut("{id:int}/update")]
    public async Task<IActionResult> updateLearningTask(int id, LearningTaskRequest request)
    {
         await _learning_task_service.updateTask(id,request);
         _logger.LogInformation($"\nStatus Code:200\nmessage: Learning Task updated sucessfully\npath: put - api/learning-task/{id}");
        return StatusCode(200, new {message=$"task with id {id} updated sucessfully"});
    }

    //get all learning tasks
    [Authorize]
    [HttpGet("all")]
    public async Task<IActionResult> GetAll([FromQuery] string? searchTerm, string? status, int pageNumber = 1, int pageSize = 10)
    {
        if (searchTerm == null) searchTerm = string.Empty;
        if (status == null) status = string.Empty;
        if (pageNumber < 1) { pageNumber = 1; }
        if (pageSize < 1) { pageSize = 5; }
        GetAllDTO<LearningTaskResponse> response = await _learning_task_service.getLearningTasks(searchTerm, status, pageNumber, pageSize);
        _logger.LogInformation($"\nStatus Code:200\nmessage: Task List Fetched Sucessfully");
        return StatusCode(200, new { response, message = "Task List Fetched Sucessfully!" });
        
    }
}

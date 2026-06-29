
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trainee_management.Exceptions;
using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;
using trainee_management.Services;
namespace trainee_management.Controllers;

[ApiController]
[Route("api/trainee")]
public class TraineeController : ControllerBase
{
    private readonly ItraineeService _traineeService;
    private readonly ILogger<TraineeController> _logger;
    public TraineeController(ItraineeService traineeService,ILogger<TraineeController> logger)
    {
        _traineeService=traineeService;
        _logger=logger;
    }


    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? searchTerm,string? status,int pageNumber=1,int pageSize=10)
    {
        if(searchTerm==null) searchTerm=string.Empty;
        if(status==null) status=string.Empty;
        if(pageNumber<1) throw new ValidationException("Invalid page number");
        if(pageSize<1)  throw new ValidationException("Invalid page size");
        GetAllDTO<TraineeResponse> response=await _traineeService.ReturnTrainees(searchTerm,status,pageNumber,pageSize);
        _logger.LogInformation("\nStatus Code:200\nmessage: Trainee List Fetched Sucessfully");
        return StatusCode(200, new { response, message = "Trainee List Fetched Sucessfully!" });
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateTraineeRequest request)
    {   
        if(request.Email==null) return StatusCode(400,new {error="Email not provided"});
        await _traineeService.CheckIfTraineeExists(request.Email);
        await _traineeService.CreateTrainee(request);
        _logger.LogInformation($"Status Code:201 message: Trainee Added to database");
        return StatusCode(201, new {  message = "Trainee Added to Database" });
    }


    //get trainee details 
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTraineeDetails(int id)
    {
        Trainee trainee=await _traineeService.GetTraineeById(id)
         ??  throw new NotFoundException("Trainee Not found");
        TraineeResponse response=_traineeService.GetTraineeResponse(trainee);
        _logger.LogInformation($"Status Code:200 message: Trainee returned sucessfully\npath: get - api/Trainee/{id}");
        return StatusCode(200,new {trainee=response,message=$"Trainee with id {id} returned sucessfully"});
    }


    //update trainee
    [Authorize]
    [HttpPut("{id:int}/update")]
    public async Task<IActionResult> UpdateTraineeDetails(int id,UpdateTraineeRequest request)
    {
        if(request==null) throw new ArgumentNullException("Invalid data is provided , null request .");
        await _traineeService.UpdateTrainee(request,id);
        _logger.LogInformation($"Status Code:200 message: Trainee updated sucessfully path: put - api/Trainee/{id}");
        return StatusCode(200,new {message="Trainee Updated Sucessfully"});
    }

    //delete trainee
    [Authorize]
    [HttpDelete("{id:int}/delete")]
    public async Task<IActionResult> DeleteTrainee(int id)
    {
        Trainee trainee=await _traineeService.GetTraineeById(id)
        ?? throw new NotFoundException("Trainee Not found");
        await _traineeService.DeleteTrainee(trainee);
        _logger.LogInformation($"Status Code:204 message: Trainee deleted sucessfully path: delete - api/Trainee/{id}");
        return StatusCode(204);    
    }

    
}

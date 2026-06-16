
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
        GetAllDTO<TraineeResponse> response=await _traineeService.returnTrainees(searchTerm,status,pageNumber,pageSize);
        _logger.LogInformation($"\nStatus Code:200\nmessage: Trainee List Fetched Sucessfully");
        return StatusCode(200, new { response, message = "Trainee List Fetched Sucessfully!" });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateTraineeRequest request)
    {   
        if(request.Email==null) return StatusCode(400,new {error="Email not provided"});
        await _traineeService.checkIfTraineeExists(request.Email);
        await _traineeService.createTrainee(request);
        _logger.LogInformation($"\nStatus Code:201\nmessage: Trainee Added to database");
        return StatusCode(201, new {  message = "Trainee Added to Database" });
    }


    //get trainee details 
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTraineeDetails(int id)
    {
        Trainee trainee=await _traineeService.getTraineeById(id)
         ??  throw new NotFoundException("Trainee Not found");
        TraineeResponse response=_traineeService.getTraineeResponse(trainee);
        _logger.LogInformation($"\nStatus Code:200\nmessage: Trainee returned sucessfully\npath: get - api/Trainee/{id}");
        return StatusCode(200,new {trainee=response,message=$"Trainee with id {id} returned sucessfully"});
    }


    //update trainee
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTraineeDetails(int id,UpdateTraineeRequest request)
    {
        if(request==null) throw new ArgumentNullException("Invalid data is provided , null request .");
        Trainee trainee=await _traineeService.getTraineeById(id)
        ?? throw new NotFoundException("Trainee Not found");
        await _traineeService.updateTrainee(request,trainee);
        _logger.LogInformation($"\nStatus Code:200\nmessage: Trainee updated sucessfully\npath: put - api/Trainee/{id}");
        return StatusCode(200,new {message="Trainee Updated Sucessfully"});
    }

    //delete trainee
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTrainee(int id)
    {
        Trainee? trainee=await _traineeService.getTraineeById(id)
        ?? throw new NotFoundException("Trainee Not found");
        await _traineeService.deleteTrainee(trainee);
        _logger.LogInformation($"\nStatus Code:204\nmessage: Trainee deleted sucessfully\npath: delete - api/Trainee/{id}");
        return StatusCode(204);    
    }

    
}

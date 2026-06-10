
using Microsoft.AspNetCore.Mvc;
using trainee_management.Exceptions;
using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;
using trainee_management.Services;
namespace trainee_management.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TraineeController : ControllerBase
{
    private readonly ItraineeService _traineeService;
    public TraineeController(ItraineeService traineeService)
    {
        _traineeService=traineeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string searchTerm)
    {
        if(searchTerm==null) searchTerm=string.Empty;
        List<TraineeResponse> trainees=await _traineeService.returnTrainees(searchTerm);
        return StatusCode(200, new { traineesList=trainees, message = "Trainee List Fetched Sucessfully!" });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTraineeRequest request)
    {   
        if(request.Email==null) return StatusCode(400,new {error="Email not provided"});
        await _traineeService.checkIfTraineeExists(request.Email);
        await _traineeService.createTrainee(request);
        return StatusCode(201, new {  message = "Trainee Added to Database" });
    }


    //get trainee details 

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTraineeDetails(int id)
    {
        Trainee? trainee=await _traineeService.getTraineeById(id);
        if (trainee == null)
        {
            throw new NotFoundException("Trainee Not found");
        }
        TraineeResponse response=_traineeService.getTraineeResponse(trainee);
        return StatusCode(200,new {trainee=response,message=$"Trainee with id {id} returned sucessfully"});
    }


    //update trainee
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTraineeDetails(int id,UpdateTraineeRequest request)
    {
        Trainee? trainee=await _traineeService.getTraineeById(id);
        if (trainee == null)
        {
            throw new NotFoundException("Trainee Not found");
        }
        bool traineeUpdated=await _traineeService.updateTrainee(request,trainee);
        if (traineeUpdated)
        {
            return StatusCode(200,new {message="Trainee Updated Sucessfully"});
        }
        return StatusCode(400, new {error=$"Failed to update trainee with id ${id}"});
        
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTrainee(int id)
    {
        Trainee? trainee=await _traineeService.getTraineeById(id);
        if (trainee == null)
        {
            throw new NotFoundException("Trainee Not found");
        }
        await _traineeService.deleteTrainee(trainee);
        return StatusCode(204);    
    }

    
}


using Microsoft.AspNetCore.Mvc;
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
    public IActionResult GetAll()
    {
        return StatusCode(200, new { traineesList=_traineeService.returnTrainees(), message = "Trainee List Fetched Sucessfully !" });
    }

    [HttpPost]
    public IActionResult Create(CreateTraineeRequest request)
    {   
   
        Trainee trainee=_traineeService.getTraineeByEmail(request.Email);
        if (trainee != null)
        {
            return StatusCode(400, new { error = "A Trainee with this email already exists" });
        }

    
        TraineeResponse response=_traineeService.createTrainee(request);
        return StatusCode(201, new { response, message = "Trainee Added to Database" });
        
        
    }


    //get trainee details 

    [HttpGet("{id:int}")]
    public IActionResult GetTraineeDetails(int id)
    {
        Trainee trainee=_traineeService.getTraineeById(id);
        if (trainee == null)
        {
            return StatusCode(400,new {error="Trainee not found with this id"});
        }

        TraineeResponse response=_traineeService.getTraineeResponse(trainee);
        return StatusCode(200,new {trainee=response,message=$"Trainee with id {id} returned sucessfully"});
    }


    //update trainee
    [HttpPut("{id:int}")]
    public IActionResult UpdateTraineeDetails(int id,UpdateTraineeRequest request)
    {
        Trainee trainee=_traineeService.getTraineeById(id);
        if (trainee == null)
        {
            return StatusCode(404,new {error="Trainee not found"});
        }
        TraineeResponse updatedTrainee=_traineeService.updateTrainee(request,trainee);
        return StatusCode(200,new {updatedTrainee,message="Trainee Updated Sucessfully"});
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteTrainee(int id)
    {
        Trainee trainee=_traineeService.getTraineeById(id);
        if (trainee == null)
        {
            return StatusCode(404,new {error="Trainee not found try another id"});
        }

        TraineeResponse deletedTrainee=_traineeService.deleteTrainee(trainee);

        return StatusCode(204,new {message="Trainee Deleted Sucessfully",deletedTrainee});

    }

}

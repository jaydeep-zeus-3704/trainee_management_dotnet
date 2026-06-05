
using Microsoft.AspNetCore.Mvc;
using trainee_management.Models;
namespace trainee_management.Controllers;
    [ApiController]
    [Route("api/[controller]")]
    public class TraineeController : ControllerBase
    {
        public static List<Trainee> trainees=[];
        public static int employeeId=0;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(trainees);
        }

        [HttpPost]
        public IActionResult Create(Trainee trainee)
        {
            

            trainee.Id=employeeId++;
            trainees.Add(trainee);
            return Ok(trainee);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTraineeDetails(int id){
                Console.WriteLine(trainees.Count);
                Trainee? trainee=trainees.Find(t=>t.Id==id);
                if (trainee == null)
                {
                    return NotFound("trainee not found");
                    
                }
                return Ok(trainee);
        }
    }

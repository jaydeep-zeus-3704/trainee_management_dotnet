
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trainee_management.Services;

[ApiController]
[Route("api/submissions")]
public class SubmissionController : ControllerBase
{
   private readonly ISubmissionService _submission_service;
   private readonly ILogger<SubmissionController> _logger;
   public SubmissionController(ISubmissionService submission_service,ILogger <SubmissionController> logger)
   {
    _submission_service=submission_service;
    _logger=logger;
   }


    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateSubmission(SubmissionRequest request)
    {
        await _submission_service.CreateSubmission(request);
        _logger.LogInformation($"\nStatus Code:201\nmessage: submissions created Sucessfully");
        return StatusCode(201,new {message="Submission Created"});
    } 


    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<SubmissionResponse>submissions=await _submission_service.GetAllSubmissions();
        _logger.LogInformation($"\nStatus Code:200\nmessage: submissions List Fetched Sucessfully");
        return StatusCode(200,new {submissions,message="submissions fetched sucessfully"});
    } 

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSubmissionById(int id)
    {
        SubmissionResponse submission=await _submission_service.GetSubmission(id);
        _logger.LogInformation($"\nStatus Code:200\nmessage: submission for id {id}  Fetched Sucessfully");
        return StatusCode(200,new {submission,message="submissions fetched sucessfully"});
    } 


}
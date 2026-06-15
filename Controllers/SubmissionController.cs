
using Microsoft.AspNetCore.Mvc;
using trainee_management.Services;

[ApiController]
[Route("api/submissions")]
public class SubmissionController : ControllerBase
{
   private readonly ISubmissionService _submission_service;
   public SubmissionController(ISubmissionService submission_service)
   {
    _submission_service=submission_service;
   }


    [HttpPost]
    public async Task<IActionResult> CreateSubmission(SubmissionRequest request)
    {
        await _submission_service.CreateSubmission(request);
        return StatusCode(201,new {message="Submission Created"});
    } 

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<SubmissionResponse>submissions=await _submission_service.GetAllSubmissions();
        return StatusCode(200,new {submissions,message="submissions fetched sucessfully"});
    } 

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSubmissionById(int id)
    {
        SubmissionResponse submission=await _submission_service.GetSubmission(id);
        return StatusCode(200,new {submission,message="submissions fetched sucessfully"});
    } 


}
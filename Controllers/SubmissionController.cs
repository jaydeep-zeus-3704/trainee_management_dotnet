
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trainee_management.Models.Entities;
using trainee_management.Services;

[ApiController]
[Authorize]
[Route("api/submissions")]
public class SubmissionController : ControllerBase
{
   private readonly ISubmissionService _submission_service;
   private readonly IFileStorageSerivce _file_storage_service;
   private readonly ILogger<SubmissionController> _logger;
   public SubmissionController(ISubmissionService submission_service,IFileStorageSerivce file_storage_service, ILogger <SubmissionController> logger)
   {
    _submission_service=submission_service;
    _logger=logger;
    _file_storage_service=file_storage_service;
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

    
    [HttpPost("{submissionId:int}/files")]
      public async Task<IActionResult> UploadFile(IFormFile file,int submissionId)
    {   
        int userId=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        SubmissionFilesResponse response=await _file_storage_service.SaveAsync(file,userId,submissionId);
        return StatusCode(201,new {response,message="File Uploaded sucessfully"});
    }
    

}
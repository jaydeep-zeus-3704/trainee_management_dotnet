
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/submissions")]
public class SubmissionController : ControllerBase
{
    
    [HttpPost]
    public Task<IActionResult> CreateSubmission()
    {
        
        throw new NotImplementedException("");
    } 


}
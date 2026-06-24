
using System.Net.Sockets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trainee_management.Database;
using trainee_management.Exceptions;

[ApiController]
[Route("api/processing-jobs")]
public class ProcessingJobController : ControllerBase
{
    private readonly AppDBContext _context;
    public ProcessingJobController(AppDBContext context)
    {
        _context=context;
    }


    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetJobById(int id)
    {
        ProcessingJob job=await _context.ProcessingJob.FindAsync(id) ??
        throw new NotFoundException($"Processing job with id {id} not found");
        return StatusCode(StatusCodes.Status200OK,new {job,message=$"Job with id {id} fetched sucessfully"});
    }
}
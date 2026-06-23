
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using trainee_management.Models.Entities;
using trainee_management.Services;
[ApiController]
[Route("api/sumbission-files")]
public class SubmissionFilesController : ControllerBase
{

     private readonly IFileStorageSerivce _file_storage_service;
     public SubmissionFilesController(IFileStorageSerivce file_storage_service)
     {
        _file_storage_service=file_storage_service;
     }

     [HttpDelete("{id:int}/delete-file")]
    public async Task<IActionResult> DeleteFile(int id)
    {
       
        await _file_storage_service.DeleteAsync(id);
        return StatusCode(StatusCodes.Status204NoContent,new {message="File Deleted sucessfully"});
    }

    [HttpGet("{id:int}/download")]
    public async Task<IActionResult> ReadFile(int id)
    {
        FileStream stream=await _file_storage_service.OpenReadAsync(id);
        string FileName=Path.GetFileName(stream.Name);
        string extension=Path.GetExtension(FileName);
        return File(stream,$"application/{extension.Substring(1)}",FileName);
    }




}
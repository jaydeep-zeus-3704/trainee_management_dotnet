namespace trainee_management.Controllers;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;
using trainee_management.Services;

[ApiController]
[Route("api/mentor")]
public class MentorController : ControllerBase
{

    private readonly IMentorService _mentor_service;
    private readonly ILogger<MentorController> _logger;
    public MentorController(IMentorService mentor_service, ILogger<MentorController> logger)
    {
        _mentor_service = mentor_service;
        _logger = logger;
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? searchTerm, string? status, int pageNumber = 1, int pageSize = 10)
    {
        if (searchTerm == null) searchTerm = string.Empty;
        if (status == null) status = string.Empty;
        if(pageNumber<1) throw new ValidationException("Invalid page number");
        if(pageSize<1)  throw new ValidationException("Invalid page size");
        GetAllDTO<MentorResponse> response = await _mentor_service.GetMentors(searchTerm, status, pageNumber, pageSize);
        _logger.LogInformation($"\nStatus Code:200\nmessage: Mentor List Fetched Sucessfully");
        return StatusCode(200, new { response, message = "Mentor List Fetched Sucessfully!" });
    }
    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> createMentor(MentorRequest request)
    {
        await _mentor_service.CreateMentor(request);
        _logger.LogInformation($"\nStatus Code:201\nmessage: mentor created sucessfully\npath: get - api/mentor/create");
        return StatusCode(201, new { message = "Mentor created sucessfully" });
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> getMentorById(int id)
    {
        MentorResponse response = await _mentor_service.GetMentorById(id);
        _logger.LogInformation($"\nStatus Code:200\nmessage: mentor returned sucessfully\npath: get - api/mentor/{id}");
        return StatusCode(200, new { mentor = response, message = $"Mentor with id {id} returned sucessfully" });
    }

    [Authorize]
    [HttpPut("{id:int}/update")]
    public async Task<IActionResult> updateMentor(int id, MentorRequest request)
    {
        await _mentor_service.UpdateMentor(id, request);
        _logger.LogInformation($"\nStatus Code:200\nmessage: Mentor updated sucessfully\npath: put - api/mentor/{id}");
        return StatusCode(200, new { message = "Mentor Updated Sucessfully" });
    }


    [Authorize]
    [HttpDelete("{id:int}/delete")]
    public async Task<IActionResult> deleteMentor(int id)
    {
        await _mentor_service.DeleteMentor(id);
        _logger.LogInformation($"\nStatus Code:204\nmessage: Mentor deleted sucessfully\npath: delete - api/mentor/{id}");
        return StatusCode(204);
    }


}
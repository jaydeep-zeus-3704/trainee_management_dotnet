
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using trainee_management.Database;
using trainee_management.Enums;
using trainee_management.Exceptions;
using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;

namespace trainee_management.Services;

public class MentorService : IMentorService
{
    private readonly AppDBContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public MentorService(AppDBContext context,IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor=httpContextAccessor;
    }

    public async Task CreateMentor(MentorRequest request)
    {
        if (_httpContextAccessor.HttpContext!=null){
                bool isMentor= _httpContextAccessor.HttpContext.User.IsInRole("MENTOR"); 
                if(!isMentor) throw new ForbidenException("User with role admin/trainee cannot perform this operation"); 
        }
        else throw new ForbidenException("Forbidden");
        
        await MentorExistsByEmail(request.Email);

        int id=int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        bool foundMentor=await _context.Mentor.AnyAsync(mentor=>mentor.Id==id);
        if(foundMentor) throw new UserAlreadyExistsException("Mentor already exists for the current Id. Mentor Information already exists"); 
        Mentor mentor = new Mentor(request,id);
        MentorValidator validator = new MentorValidator(mentor);
        if (!validator.Validate())
        {
            throw new ValidationException("Invalid details provided");
        }
        await _context.Mentor.AddAsync(mentor);
        await _context.SaveChangesAsync();
    }

    public async Task MentorExistsByEmail(string email)
    {
        Mentor? mentor = await _context.Mentor.FirstOrDefaultAsync(m => m.Email == email);
        if (mentor != null)
        {
            throw new DuplicateEmailException("Mentor with this email already exists");
        }
    }

    public async Task<GetAllDTO<MentorResponse>> GetMentors(string searchParams, string status, int pageNumber, int pageSize)
    {
        List<Mentor> mentors=[];
            if(Enum.TryParse(status,true,out MentorStatus result))
            {
                mentors=_context.Mentor.FromSqlInterpolated($"CALL GetMentors({searchParams},{(int)result},{pageNumber},{pageSize})").ToList();
            }
            else
            {
                throw new ValidationException("Invalid Status");
            }
        List<MentorResponse> mentorList =  mentors.Select(m => new MentorResponse(m)).ToList();
        GetAllDTO<MentorResponse> response = new GetAllDTO<MentorResponse>
        {
            pageNumber = pageNumber,
            pageSize = pageSize,
            totalCount = mentorList.Count,
            data = mentorList
        };
        return response;
    }

    public async Task<MentorResponse> GetMentorById(int id)
    {
        Mentor mentor = await _context.Mentor.FindAsync(id)
        ?? throw new NotFoundException("mentor Not found");
        return new MentorResponse(mentor);
    }

    public async Task UpdateMentor(int id,MentorRequest request)
    {
        Mentor mentor = await _context.Mentor.FindAsync(id)
         ?? throw new NotFoundException("mentor Not found");
        mentor.FirstName=request.FirstName;
        mentor.LastName=request.LastName;
        mentor.Email=request.Email;
        mentor.Expertise=request.Expertise;
        mentor.Status=request.Status;
        mentor.updatedAt=DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMentor(int id)
    {
        Mentor mentor = await _context.Mentor.FindAsync(id)
        ?? throw new NotFoundException("mentor Not found");
         _context.Mentor.Remove(mentor);
         await _context.SaveChangesAsync();
    }
}
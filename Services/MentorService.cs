
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
    public MentorService(AppDBContext context)
    {
        _context = context;
    }

    public async Task createMentor(MentorRequest request)
    {
        await mentorExistsByEmail(request.Email);
        Mentor mentor = new Mentor(request);
        MentorValidator validator = new MentorValidator(mentor);
        if (!validator.Validate())
        {
            throw new ValidationException("Invalid details provided");
        }
        await _context.Mentor.AddAsync(mentor);
        await _context.SaveChangesAsync();
    }

    public async Task mentorExistsByEmail(string email)
    {
        Mentor? mentor = await _context.Mentor.FirstOrDefaultAsync(m => m.Email == email);
        if (mentor != null)
        {
            throw new DuplicateEmailException("Mentor with this email already exists");
        }
    }

    public async Task<GetAllDTO<MentorResponse>> getMentors(string searchParams, string status, int pageNumber, int pageSize)
    {
        IQueryable<Mentor> mentors = _context.Mentor;
        mentors = await filterBySearch(searchParams, status, mentors);
        mentors = getPaginatedData(pageNumber, pageSize, mentors);
        List<MentorResponse> mentorList = await mentors.Select(m => new MentorResponse(m)).ToListAsync();
        GetAllDTO<MentorResponse> response = new GetAllDTO<MentorResponse>
        {
            pageNumber = pageNumber,
            pageSize = pageSize,
            totalCount = mentorList.Count,
            data = mentorList
        };
        return response;
    }


    public async Task<IQueryable<Mentor>> filterBySearch(string searchParams, string status, IQueryable<Mentor> mentors)
    {
        if (!string.IsNullOrWhiteSpace(searchParams))
        {
            searchParams = searchParams.Trim().ToLower();
            mentors = mentors.Where(t =>
                t.FirstName.ToLower().Contains(searchParams) ||
                t.LastName.ToLower().Contains(searchParams) ||
                t.Email.ToLower().Contains(searchParams) ||
                t.Expertise.ToLower().Contains(searchParams)
            );
        }

        if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<MentorStatus>(status, true, out var result))
        {
            mentors = mentors.Where(t => t.Status == result);
        }
        return mentors;
    }

    //pagination
    public IQueryable<Mentor> getPaginatedData(int pageNumber, int pageSize, IQueryable<Mentor> mentors)
    {
        mentors = mentors.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        return mentors;
    }

    public async Task<MentorResponse> getMentorById(int id)
    {
        Mentor? mentor = await _context.Mentor.FindAsync(id);
        if (mentor == null)
        {
            throw new NotFoundException("mentor Not found");
        }
        return new MentorResponse(mentor);
    }

    public async Task updateMentor(int id,MentorRequest request)
    {
        Mentor? mentor = await _context.Mentor.FindAsync(id);
        if (mentor == null)
        {
            throw new NotFoundException("mentor Not found");
        }
        mentor.FirstName=request.FirstName;
        mentor.LastName=request.LastName;
        mentor.Email=request.Email;
        mentor.Expertise=request.Expertise;
        mentor.Status=request.Status;
        mentor.updatedAt=DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task deleteMentor(int id)
    {
        Mentor? mentor = await _context.Mentor.FindAsync(id);
        if (mentor == null)
        {
            throw new NotFoundException("mentor Not found");
        }
         _context.Mentor.Remove(mentor);
         await _context.SaveChangesAsync();
    }
}
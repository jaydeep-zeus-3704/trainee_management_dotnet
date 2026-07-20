
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using trainee_management.Database;
using trainee_management.Exceptions;
using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;
using trainee_management.Services;

public class SubmissionService : ISubmissionService
{

    private readonly AppDBContext _context;
    private readonly ICacheService _cache_service;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SubmissionService(AppDBContext context, ICacheService cache_service, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _cache_service = cache_service;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task CreateSubmission(SubmissionRequest request)
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            bool isTrainee = _httpContextAccessor.HttpContext.User.IsInRole("TRAINEE");
            if (!isTrainee) throw new NotFoundException("Forbidden, User with role admin/mentor cannot perform this operation");
        }
        else
        {
            throw new ForbidenException("Forbidden");
        }
        int traineeId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        bool taskExists = await _context.TaskAssignment.AsNoTracking().AnyAsync(t => t.Id == request.TaskAssignmentId);
        if (!taskExists) throw new NotFoundException("Task Assignment doesn't exist");
        Submission submission = new Submission(request, traineeId);
        await _context.Submission.AddAsync(submission);
        await _context.SaveChangesAsync();
    }

    public async Task<List<SubmissionResponse>> GetAllSubmissions()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
            throw new UnauthorizedAccessException("User not authenticated");

        bool isMentor = user.IsInRole("MENTOR");
        bool isTrainee = user.IsInRole("TRAINEE");
        int currentUserId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (isMentor){
            List<TaskAssignment> taskAssignments = await _context.TaskAssignment
            .Where(assignment => assignment.MentorId == currentUserId)
            .ToListAsync();
            List<Submission> submissions=_context.Submission.FromSqlInterpolated($"CALL GetSubmissionsByMentorId({currentUserId})").ToList();
            return submissions.Select(s => new SubmissionResponse(s)).ToList();
        }

        // Ensure we only return submissions owned by this trainee
        if (isTrainee)
        {
            return await _context.Submission
                .Where(s => s.TraineeId == currentUserId)
                .Select(s => new SubmissionResponse(s))
                .ToListAsync();
        }
        else throw new ForbidenException("You dont have the required role to view user submissions");

    }


    public async Task<SubmissionResponse> GetSubmission(int id)
    {
        string key = $"submission_{id}";
        Submission? submission = await _cache_service.GetDataAsync<Submission>(key);
        if (submission == null)
        {
            submission = await _context.Submission.FindAsync(id) ?? throw new NotFoundException("Id not found");
            await _cache_service.SetDataAsync(key, submission);
        }
        SubmissionResponse res = new SubmissionResponse(submission);
        return res;
    }


}
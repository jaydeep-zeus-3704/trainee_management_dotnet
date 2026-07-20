
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using trainee_management.Database;
using trainee_management.Exceptions;
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


    public  async Task CreateSubmission(SubmissionRequest request)
    {
        if (_httpContextAccessor.HttpContext!=null)
        {
           bool isTrainee= _httpContextAccessor.HttpContext.User.IsInRole("TRAINEE");  
           if(!isTrainee) throw new NotFoundException("Forbidden, User with role admin/mentor cannot perform this operation"); 
        }
        else
        {
            throw new ForbidenException("Forbidden");
        }
        
        bool taskExists=await _context.TaskAssignment.AsNoTracking().AnyAsync(t=>t.Id==request.TaskAssignmentId);
        if(!taskExists) throw new NotFoundException("Task Assignment doesn't exist");
        Submission submission=new Submission(request);
        await _context.Submission.AddAsync(submission);
        await _context.SaveChangesAsync();
    }

    public async Task<List<SubmissionResponse>> GetAllSubmissions()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null)
            throw new UnauthorizedAccessException("User not authenticated");

        bool isMentor = user.IsInRole("Mentor");
        if (isMentor)
        {
            
            return await _context.Submission
                .Select(s => new SubmissionResponse(s))
                .ToListAsync();
        }

        int currentUserId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // Ensure we only return submissions owned by this trainee.
        return await _context.Submission
            .Include(s => s.TaskAssignment)
            .Select(s => new SubmissionResponse(s))
            .ToListAsync();

    }


    public async Task<SubmissionResponse> GetSubmission(int id)
    {   
        string key=$"submission_{id}";
        Submission? submission=await _cache_service.GetDataAsync<Submission>(key);
        if (submission == null)
        {
            submission=await _context.Submission.FindAsync(id) ?? throw new NotFoundException("Id not found");
            await _cache_service.SetDataAsync(key,submission);
        }
        SubmissionResponse res=new SubmissionResponse(submission);
        return res;
    }


}
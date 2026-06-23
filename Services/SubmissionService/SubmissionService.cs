
using Microsoft.EntityFrameworkCore;
using trainee_management.Database;
using trainee_management.Exceptions;
using trainee_management.Services;

public class SubmissionService : ISubmissionService
{
    private readonly AppDBContext _context;
    private readonly ICacheService _cache_service;
    public SubmissionService(AppDBContext context,ICacheService cache_service)
    {
        _context=context;
        _cache_service=cache_service;
    }

    public  async Task CreateSubmission(SubmissionRequest request)
    {
        bool taskExists=await _context.TaskAssignment.AsNoTracking().AnyAsync(t=>t.Id==request.TaskAssignmentId);
        if(!taskExists) throw new NotFoundException("Task Assignment doesn't exist");
        Submission submission=new Submission(request);
        await _context.Submission.AddAsync(submission);
        await _context.SaveChangesAsync();
    }

    public async Task<List<SubmissionResponse>> GetAllSubmissions()
    {
        List<SubmissionResponse> submissions=await _context.Submission.Select(s=>new SubmissionResponse(s)).ToListAsync();
        return submissions;
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
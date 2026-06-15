
using Microsoft.EntityFrameworkCore;
using trainee_management.Database;
using trainee_management.Exceptions;
using trainee_management.Services;

public class SubmissionService : ISubmissionService
{
    private readonly AppDBContext _context;
    public SubmissionService(AppDBContext context)
    {
        _context=context;
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
        Submission submission=await _context.Submission.FindAsync(id) 
        ?? throw new NotFoundException("Id not found");
        SubmissionResponse res=new SubmissionResponse(submission);
        return res;
    }


}
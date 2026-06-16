
namespace trainee_management.Services;
public interface ISubmissionService
{

    public Task CreateSubmission(SubmissionRequest request);

    public  Task<SubmissionResponse> GetSubmission(int id);

    public  Task<List<SubmissionResponse>> GetAllSubmissions();


    
}
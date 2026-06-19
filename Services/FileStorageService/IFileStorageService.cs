using trainee_management.Models.Entities;

namespace trainee_management.Services;
public interface IFileStorageSerivce
{
        public  Task<SubmissionFilesResponse> SaveAsync(IFormFile file,int userId,int submissionId);
        public Task<FileStream> OpenReadAsync(int id);
        public Task DeleteAsync(int id);


}
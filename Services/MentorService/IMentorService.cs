using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;

namespace trainee_management.Services;

public interface IMentorService
{
    public Task  CreateMentor(MentorRequest request);
    public Task<GetAllDTO<MentorResponse>> GetMentors(string searchTerm,string status,int pageNumber,int pageSize);
    public  Task<MentorResponse> GetMentorById(int id);

     public  Task UpdateMentor(int id,MentorRequest request);

       public  Task DeleteMentor(int id);

}
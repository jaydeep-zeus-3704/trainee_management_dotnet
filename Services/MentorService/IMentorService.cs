using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;

namespace trainee_management.Services;

public interface IMentorService
{
    public Task  createMentor(MentorRequest request);
    public Task<GetAllDTO<MentorResponse>> getMentors(string searchTerm,string status,int pageNumber,int pageSize);
    public  Task<MentorResponse> getMentorById(int id);

     public  Task updateMentor(int id,MentorRequest request);

       public  Task deleteMentor(int id);

}
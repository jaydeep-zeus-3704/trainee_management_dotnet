using trainee_management.Enums;
namespace trainee_management.Models.Entities;

public class MentorResponse
{
    public    string  FirstName{get;set;}=string.Empty;

    public    string  LastName{get;set;}=string.Empty;

    public  string Email{get;set;}=string.Empty;

    public   string Expertise{get;set;}=string.Empty;

    public  MentorStatus Status{get;set;}


    public MentorResponse(Mentor mentor)
    {
        FirstName=mentor.FirstName;
        LastName=mentor.LastName;
        Email=mentor.Email;
        Expertise=mentor.Expertise;
        Status=mentor.Status;
    }

    public MentorResponse()
    {
        
    }
}
using System.ComponentModel.DataAnnotations;
using trainee_management.Enums;
namespace trainee_management.Models.Entities;

public class MentorResponse
{
    [Required(ErrorMessage ="FirstName is required")]
    public    string  FirstName{get;set;}=string.Empty;

    [Required(ErrorMessage ="LastName is required")]
    public    string  LastName{get;set;}=string.Empty;

    [Required(ErrorMessage ="email is required")]
    [EmailAddress]
    public  string Email{get;set;}=string.Empty;

    [Required(ErrorMessage ="expertise is required")]
    public   string Expertise{get;set;}=string.Empty;

    [Required(ErrorMessage ="Mentor Status is required")]
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
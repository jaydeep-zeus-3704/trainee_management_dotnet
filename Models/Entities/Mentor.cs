using System.ComponentModel.DataAnnotations;
using trainee_management.Enums;
namespace trainee_management.Models.Entities;




public class Mentor
{
    public int Id{get;set;}

    [Required(ErrorMessage ="FirstName is required")]
    public    string  FirstName{get;set;}=String.Empty;

    [Required(ErrorMessage ="LastName is required")]
    public    string  LastName{get;set;}=String.Empty;

    [Required(ErrorMessage ="email is required")]
    [EmailAddress]
    public  string Email{get;set;}=string.Empty;

    [Required(ErrorMessage ="expertise is required")]
    public   string Expertise{get;set;}=string.Empty;

    [Required(ErrorMessage ="Mentor Status is required")]
    public  MentorStatus Status{get;set;}

    public DateTime createdAt{get;set;}
    public DateTime updatedAt {get;set;}


    public Mentor(MentorRequest request)
    {
        FirstName=request.FirstName;
        LastName=request.LastName;
        Expertise=request.Expertise;
        Email=request.Email;
        Status=request.Status;
        createdAt=DateTime.UtcNow;
        updatedAt=DateTime.UtcNow;
        
    }

    public Mentor()
    {
        
    }
    

}
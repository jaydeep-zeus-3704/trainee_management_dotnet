using System.ComponentModel.DataAnnotations;
using trainee_management.Enums;
namespace trainee_management.Models.Entities;




public class MentorRequest
{
    [Required(ErrorMessage ="FirstName is required")]
    public  required  string  FirstName{get;set;}

    [Required(ErrorMessage ="LastName is required")]
    public  required  string  LastName{get;set;}

    [Required(ErrorMessage ="email is required")]
    [EmailAddress]
    public required string Email{get;set;}

    [Required(ErrorMessage ="expertise is required")]
    public  required string Expertise{get;set;}

    [Required(ErrorMessage ="Mentor Status is required")]
    public required MentorStatus Status{get;set;}
}
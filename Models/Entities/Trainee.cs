using System.ComponentModel.DataAnnotations;
using trainee_management.Enums;
using trainee_management.Models.DTOs;
namespace trainee_management.Models.Entities;





public class Trainee
{
    public  int Id{get; set;}
    
    [Required(ErrorMessage ="FirstName is required")]
    [MinLength(2,ErrorMessage ="First name should contain minimum 2 characters")]
    [MaxLength(50,ErrorMessage ="First name should contain maximum 50 characters")]
    public  string FirstName {get;set;}=string.Empty;

    [Required(ErrorMessage ="LastName is required")]
    [MinLength(2,ErrorMessage ="Last name should contain minimum 2 characters")]
    [MaxLength(50,ErrorMessage ="Last name should contain maximum 50 characters")]
    public string LastName{get;set;}=string.Empty;

    [Required(ErrorMessage ="Email is required")]
    [EmailAddress]
    public string Email{get;set;}=string.Empty;
    
    [Required(ErrorMessage ="Techstack is required")]
    public  string TechStack{get;set;}=string.Empty;

    
    [Required(ErrorMessage ="Status is required ")]
    public  StatusValues Status{get;set;}
    public DateTime CreatedDate {get;set;}
    public DateTime UpdatedDate {get;set;}

    public Trainee(CreateTraineeRequest request)
    {
        FirstName=request.FirstName;
        LastName=request.LastName;
        Email=request.Email;
        TechStack=request.TechStack;
        Status=request.Status;
        CreatedDate=DateTime.Now;
        UpdatedDate=DateTime.Now;
    }

    public Trainee()
    {
        
    }


}
using System.ComponentModel.DataAnnotations;

namespace trainee_management.Models.Entities;



public class Trainee
{
    public  int Id{get; set;}
    
    [Required(ErrorMessage ="FirstName is required")]
    [MinLength(2,ErrorMessage ="First name should contain minimum 2 characters")]
    [MaxLength(50,ErrorMessage ="First name should contain maximum 50 characters")]
    public required string FirstName {get;set;}

    [Required(ErrorMessage ="LastName is required")]
    [MinLength(2,ErrorMessage ="Last name should contain minimum 2 characters")]
    [MaxLength(50,ErrorMessage ="Last name should contain maximum 50 characters")]
    public required string LastName{get;set;} 

    [Required(ErrorMessage ="Email is required")]
    [EmailAddress]
    public string? Email{get;set;}
    
    [Required(ErrorMessage ="At least one skill is required")]
    [MinLength(1,ErrorMessage ="At Least one skill is required")]
    public required string TechStack{get;set;}

    [AllowedValues("Active", "Inactive", "Completed","active","inactive","completed")]
    [Required(ErrorMessage ="Status is required ")]
    public required String Status{get;set;}
    public DateTime CreatedDate {get;set;}=DateTime.UtcNow;
    public DateTime UpdatedDate {get;set;}
}
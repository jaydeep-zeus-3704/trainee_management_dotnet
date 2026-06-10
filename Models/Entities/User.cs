using System.ComponentModel.DataAnnotations;
using trainee_management.Enums;
namespace trainee_management.Models.Entities;




public class User
{
    public int Id{get;set;}

    [Required(ErrorMessage ="Username is required")]
    public  required  string  Username{get;set;}

    [Required(ErrorMessage ="email is required")]
    [EmailAddress]
    public required string Email{get;set;}

    [Required(ErrorMessage ="password is required")]
    public  required string PasswordHash{get;set;}

    [Required(ErrorMessage ="User role is required")]
    public required UserRole Role{get;set;}

    public DateTime createdAt{get;set;}
    public DateTime updatedAt {get;set;}
}
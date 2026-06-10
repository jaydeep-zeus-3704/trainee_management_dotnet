using System.ComponentModel.DataAnnotations;
using trainee_management.Enums;
namespace trainee_management.Models.DTOs;

public class UserDTO
{

    [Required(ErrorMessage ="Username is required")]
    public  required  string  Username{get;set;}

    [Required(ErrorMessage ="email is required")]
    [EmailAddress]
    public required string Email{get;set;}

    [Required(ErrorMessage ="password is required")]
    public  required string password{get;set;}
    

}
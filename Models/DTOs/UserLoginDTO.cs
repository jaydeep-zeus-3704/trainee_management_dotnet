using System.ComponentModel.DataAnnotations;
namespace trainee_management.Models.DTOs;

public class UserLoginDTO
{

    [Required(ErrorMessage ="Username is required")]
    public  required  string  Username{get;set;}

    [Required(ErrorMessage ="password is required")]
    public  required string password{get;set;}
    

}
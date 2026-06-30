using System.ComponentModel.DataAnnotations;
namespace trainee_management.Models.DTOs;

public class UserLoginDTO
{

    [Required(ErrorMessage ="Username is required")]
    public  string  Username{get;set;}=string.Empty;

    [Required(ErrorMessage ="password is required")]
    public  string Password{get;set;}=string.Empty;
    

}
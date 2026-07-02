using System.ComponentModel.DataAnnotations;
using trainee_management.Constants;
using trainee_management.Enums;
namespace trainee_management.Models.Entities;




public class User
{
    public int Id{get;set;}
   [Required(ErrorMessage =StringConstant.USERNAME_REQUIRED)]
    [MinLength(2,ErrorMessage =StringConstant.USERNAME_MIN_CHARACTER)]
    [MaxLength(50,ErrorMessage =StringConstant.USERNAME_MAX_CHARACTER)]
    public string Username{get;set;}=string.Empty;

    [Required(ErrorMessage =StringConstant.EMAIL_REQUIRED)]
    [Length(6,100,ErrorMessage =StringConstant.EMAIL_CHARACTER_LIMIT)]
    [EmailAddress]
    public  string Email{get;set;}=string.Empty;

    [Required(ErrorMessage =StringConstant.PASSWORD_REQUIRED)]
    public  required string PasswordHash{get;set;}

    [Required(ErrorMessage =StringConstant.STATUS_REQUIRED)]
    [EnumDataType(typeof(UserRole),ErrorMessage =StringConstant.VALID_STATUS_REQUIRED)]
    public required UserRole Role{get;set;}

    public DateTime createdAt{get;set;}
    public DateTime updatedAt {get;set;}
}
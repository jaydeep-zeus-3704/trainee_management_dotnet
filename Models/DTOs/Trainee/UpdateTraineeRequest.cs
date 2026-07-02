using System.ComponentModel.DataAnnotations;
using trainee_management.Constants;
using trainee_management.Enums;

namespace trainee_management.Models.DTOs
{
    public class UpdateTraineeRequest
    {
    [Required(ErrorMessage =StringConstant.FIRST_NAME_REQUIRED)]
    [MinLength(2,ErrorMessage =StringConstant.FIRST_NAME_MIN_CHARACTER)]
    [MaxLength(50,ErrorMessage =StringConstant.FIRST_NAME_MAX_CHARACTER)]
    public  string FirstName {get;set;}=string.Empty;

    [Required(ErrorMessage =StringConstant.LAST_NAME_REQUIRED)]
    [MinLength(2,ErrorMessage =StringConstant.LAST_NAME_MIN_CHARACTER)]
    [MaxLength(50,ErrorMessage =StringConstant.LAST_NAME_MAX_CHARACTER)]
    public string LastName{get;set;}=string.Empty;



    [Required(ErrorMessage =StringConstant.EMAIL_REQUIRED)]
    [Length(6,100,ErrorMessage =StringConstant.EMAIL_CHARACTER_LIMIT)]
    [EmailAddress]
    public string Email{get;set;}=string.Empty;
    
    [Required(ErrorMessage =StringConstant.TECH_STACK_REQUIRED)]
    public  string TechStack{get;set;}=string.Empty;

    
    [Required(ErrorMessage =StringConstant.STATUS_REQUIRED)]
    [EnumDataType(typeof(StatusValues),ErrorMessage =StringConstant.VALID_STATUS_REQUIRED)]
    public  StatusValues Status{get;set;}

    }
}
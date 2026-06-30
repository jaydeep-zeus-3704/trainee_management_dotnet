using System.ComponentModel.DataAnnotations;
using trainee_management.Constants;
using trainee_management.Enums;
namespace trainee_management.Models.Entities;




public class Mentor
{
    public int Id{get;set;}

    [Required(ErrorMessage =StringConstant.FIRST_NAME_REQUIRED)]
    [MinLength(2,ErrorMessage =StringConstant.FIRST_NAME_MIN_CHARACTER)]
    [MaxLength(50,ErrorMessage =StringConstant.FIRST_NAME_MAX_CHARACTER)]
    public string FirstName{get;set;}=string.Empty;

    [Required(ErrorMessage =StringConstant.LAST_NAME_REQUIRED)]
    [MinLength(2,ErrorMessage =StringConstant.LAST_NAME_MIN_CHARACTER)]
    [MaxLength(50,ErrorMessage =StringConstant.LAST_NAME_MAX_CHARACTER)]

    public    string  LastName{get;set;}=string.Empty;

    [Required(ErrorMessage =StringConstant.EMAIL_REQUIRED)]
    [Length(6,100,ErrorMessage =StringConstant.EMAIL_CHARACTER_LIMIT)]
    [EmailAddress]
    public  string Email{get;set;}=string.Empty;

    [Required(ErrorMessage =StringConstant.EXPERTISE_REQUIRED)]
    [MaxLength(200,ErrorMessage =StringConstant.EXPERTISE_CHARACTER_LIMIT)]
    public   string Expertise{get;set;}=string.Empty;

    [Required(ErrorMessage =StringConstant.STATUS_REQUIRED)]
    [EnumDataType(typeof(MentorStatus),ErrorMessage =StringConstant.VALID_STATUS_REQUIRED)]
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
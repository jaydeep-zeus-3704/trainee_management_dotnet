using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using trainee_management.Constants;
using trainee_management.Enums;
using trainee_management.Models.Entities;

public class Submission
{
    [Key]
    public int Id {get;set;}
    

    [Required(ErrorMessage =StringConstant.TASK_ASSIGNMENT_ID_REQUIRED)]
    [ForeignKey("Id")]
    public int TaskAssignmentId {get;set;}
    public  TaskAssignment? TaskAssignment{get;set;}
    
    [Required(ErrorMessage =StringConstant.SUBMISSION_URL_REQUIRED)]
    [Length(4,200,ErrorMessage =StringConstant.SUBMISSION_URL_CHARACTER_LIMIT)]
    public string SubmissionUrl{get;set;}=string.Empty;
    

    [Required(ErrorMessage =StringConstant.NOTES_REQUIRED)]
    [Length(5,200,ErrorMessage =StringConstant.NOTES_CHARACTER_LIMIT)]
    public string Notes{get;set;}=string.Empty;

    [Required(ErrorMessage =StringConstant.SUBMISSION_DATE_REQUIRED)]
    public DateOnly SubmittedDate {get;set;}

    [Required(ErrorMessage =StringConstant.STATUS_REQUIRED)]
    [EnumDataType(typeof(SubmittedStatus),ErrorMessage =StringConstant.VALID_STATUS_REQUIRED)]
    public SubmittedStatus Status{get;set;}

    public Submission(SubmissionRequest request)
    {
        TaskAssignmentId=request.TaskAssignmentId;
        SubmissionUrl=request.SubmissionUrl;
        Notes=request.Notes;
        SubmittedDate=request.SubmittedDate;
        Status=request.Status;
    }

    public Submission()
    {
        
    }
    
}
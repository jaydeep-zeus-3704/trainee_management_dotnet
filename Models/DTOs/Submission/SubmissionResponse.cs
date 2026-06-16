using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using trainee_management.Enums;

public class SubmissionResponse
{
    public int Id {get;set;}

    [Required(ErrorMessage ="Task assignment id is required")]
    [ForeignKey("Id")]
    public int TaskAssignmentId {get;set;}
    
    [Required(ErrorMessage ="SubmissionUrl is required")]
    public string SubmissionUrl{get;set;}=string.Empty;
    
    [Required(ErrorMessage ="Notes are required")]
    [Length(5,50,ErrorMessage ="Notes should be between minimum 5 characters and maximum 50 characters")]
    public string Notes{get;set;}=string.Empty;

    [Required(ErrorMessage ="SubmittedDate required")]
    public DateOnly SubmittedDate {get;set;}

    [Required(ErrorMessage ="Status is required")]
    public SubmittedStatus Status{get;set;}
    
    public SubmissionResponse(Submission s)
    {
        TaskAssignmentId=s.TaskAssignmentId;
        SubmissionUrl=s.SubmissionUrl;
        Notes=s.Notes;
        SubmittedDate=s.SubmittedDate;
        Status=s.Status;
    }

    public SubmissionResponse()
    {
        
    }
}
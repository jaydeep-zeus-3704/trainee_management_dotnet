using System.ComponentModel.DataAnnotations;
using trainee_management.Enums;

public class ReviewRequest
{
    [Key]
    public int Id {get;set;}


    [Required(ErrorMessage ="Submission  id is required")]
    public int SubmissionId {get;set;}
    public  Submission? Submission{get;set;}
    
    [Required(ErrorMessage ="Mentor  id is required")]
    public int MentorId {get;set;}
    
    [Required(ErrorMessage ="Mentor feedback is required")]
    [Length(5,150,ErrorMessage ="feedback should be between minimum 5 characters and maximum 50 characters")]
    public string MentorFeedback{get;set;}=string.Empty;

    [Required(ErrorMessage ="Reviewed Date required")]
    public DateOnly ReviewedDate {get;set;}

    [Required(ErrorMessage ="Status is required")]
    public ReviewStatus Status{get;set;}

    public int? Score;
    
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using trainee_management.Enums;
using trainee_management.Models.Entities;

public class Review
{
    [Key]
    public int Id {get;set;}

    [Required(ErrorMessage ="Submission  id is required")]
    [ForeignKey("Id")]
    public int SubmissionId {get;set;}
    public  Submission? Submission{get;set;}
    
    [Required(ErrorMessage ="Mentor  id is required")]
    [ForeignKey("Id")]
    public int MentorId {get;set;}
    public  Mentor? Mentor{get;set;}
    
    [Required(ErrorMessage ="Mentor feedback is required")]
    [Length(5,150,ErrorMessage ="feedback should be between minimum 5 characters and maximum 50 characters")]
    public string MentorFeedback{get;set;}=string.Empty;

    [Required(ErrorMessage ="Reviewed Date required")]
    public DateOnly ReviewedDate {get;set;}

    [Required(ErrorMessage ="Status is required")]
    public ReviewStatus Status{get;set;}

    public int? Score;

    public Review(ReviewRequest request)
    {
        SubmissionId=request.SubmissionId;
        MentorId=request.MentorId;
        MentorFeedback=request.MentorFeedback;
        ReviewedDate=request.ReviewedDate;
        Status=request.Status;
        Score=request.Score;
    }

    public Review()
    {
        
    }

    
    
}
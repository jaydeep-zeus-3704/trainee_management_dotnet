using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using trainee_management.Constants;
using trainee_management.Enums;
using trainee_management.Models.Entities;

public class Review
{
    [Key]
    public int Id {get;set;}

    [Required(ErrorMessage =StringConstant.SUBMISSION_ID_REQUIRED)]
    public int SubmissionId {get;set;}
    public  Submission? Submission{get;set;}
    
    [Required(ErrorMessage =StringConstant.MENTOR_ID_REQUIRED)]
    public int MentorId {get;set;}
    public  Mentor? Mentor{get;set;}
    
    [Required(ErrorMessage =StringConstant.FEED_BACK_REQUIRED)]
    [MaxLength(200,ErrorMessage =StringConstant.FEED_BACK_CHARACTER_LIMIT)]
    public string MentorFeedback{get;set;}=string.Empty;

    [Required(ErrorMessage =StringConstant.REVIEWED_DATE_REQUIRED)]
    public DateOnly ReviewedDate {get;set;}
    
    [EnumDataType(typeof(ReviewStatus),ErrorMessage =StringConstant.VALID_STATUS_REQUIRED)]
    [Required(ErrorMessage =StringConstant.STATUS_REQUIRED)]
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
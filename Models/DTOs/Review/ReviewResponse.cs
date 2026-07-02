using trainee_management.Enums;

public class ReviewResponse
{
    public int Id {get;set;}
    public int SubmissionId {get;set;}
    
    public int MentorId {get;set;}
    
    public string MentorFeedback{get;set;}=string.Empty;

    public DateOnly ReviewedDate {get;set;}

    public ReviewStatus Status{get;set;}

    public int? Score;


    public ReviewResponse(Review r)
    {
        SubmissionId=r.SubmissionId;
        MentorId=r.MentorId;
        MentorFeedback=r.MentorFeedback;
        ReviewedDate=r.ReviewedDate;
        Status=r.Status;
        Score=r.Score;
    }

    public ReviewResponse()
    {
        
    }
    
}
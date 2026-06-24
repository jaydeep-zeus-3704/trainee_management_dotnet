
using System.ComponentModel.DataAnnotations;
using trainee_management.Enums;

public class ProcessingJob
{
    public int Id{get;set;}

    public int Attempts {get;set;}

    public JobStatus Status {get;set;}

    public Guid CorrelationId {get;set;}


    [Length(5,200,ErrorMessage ="ErrorSummary must be between 5 to 200 characters")]
    public string ErrorSummary {get;set;}=string.Empty;

    public DateTime StartedAt {get;set;}
    public DateTime CompletedAt{get;set;}
}
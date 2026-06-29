using System.ComponentModel.DataAnnotations;
using trainee_management.Enums;

namespace trainee_management.Models;

public class LearningTaskResponse
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ExpectedTechStack { get; set; } = string.Empty;
    public DateOnly DueDate { get; set; }
    public LearningTaskStatus Status { get; set; }

    public LearningTaskResponse(LearningTask learningTask)
    {
        Title = learningTask.Title;
        Description = learningTask.Description;
        ExpectedTechStack = learningTask.ExpectedTechStack;
        Status = learningTask.Status;
        DueDate = learningTask.DueDate;
    }

    public LearningTaskResponse()
    {

    }
}
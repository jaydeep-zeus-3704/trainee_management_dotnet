using System.ComponentModel.DataAnnotations;
using trainee_management.Enums;

namespace trainee_management.Models;

public class LearningTask
{
    public int Id{get;set;}

    [Required(ErrorMessage ="Title is required")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 20 characters.")]
    public string Title{get;set;}=string.Empty;

    
    [Required(ErrorMessage ="Description is required")]
    [StringLength(150, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 20 characters.")]
    public string Description{get;set;}=string.Empty;
    
    [Required(ErrorMessage ="ExpectedTechStack is required")]
    public string ExpectedTechStack{get;set;}=string.Empty;

    [Required(ErrorMessage ="Due Date is required")]
    public DateOnly DueDate {get;set;}

    public LearningTaskStatus Status {get;set;}

    public DateTime createdAt{get;set;}
    public DateTime updatedAt{get;set;}

    public LearningTask(LearningTaskRequest learningTask)
    {
        Title=learningTask.Title;
        Description=learningTask.Description;
        ExpectedTechStack=learningTask.ExpectedTechStack;
        Status=learningTask.Status;
        DueDate=learningTask.DueDate;
        createdAt=DateTime.Now;
        updatedAt=DateTime.Now;
    }

    public LearningTask()
    {
        
    }


}
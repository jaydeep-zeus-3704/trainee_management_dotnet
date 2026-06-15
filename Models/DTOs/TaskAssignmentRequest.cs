
namespace trainee_management.Models.DTOs;

using System.ComponentModel.DataAnnotations;
using trainee_management.Enums;

public class TaskAssignmentRequest
{
    
    public required int TraineeId {get;set;}
    public required int MentorId{get;set;}

    public required int LearningTaskId{get;set;}

    [Required(ErrorMessage ="Due Date is required")]
    public DateOnly DueDate {get;set;}

    [Required(ErrorMessage ="Assigned Date is required")]
    public DateOnly AssignedDate {get;set;}
    
    [Required(ErrorMessage ="Task assignment status is required")]
    public required TaskAssignmentStatus Status{get;set;}

    public  string Remarks{get;set;}=string.Empty;
}


public class TaskAssignmentUpdate
{
    public required TaskAssignmentStatus Status {get;set;}
}



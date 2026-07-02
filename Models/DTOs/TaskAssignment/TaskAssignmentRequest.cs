
namespace trainee_management.Models.DTOs;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using trainee_management.Constants;
using trainee_management.Enums;

public class TaskAssignmentRequest
{
    
    [Required(ErrorMessage =StringConstant.TRAINEE_ID_REQUIRED)]
    public  int TraineeId {get;set;}

   [Required(ErrorMessage =StringConstant.LEARNING_TASK_ID_REQUIRED)]
    
    public  int LearningTaskId{get;set;}

    public  LearningTask? LearningTask {get;set;}

    [Required(ErrorMessage =StringConstant.MENTOR_ID_REQUIRED)]
    public  int MentorId{get;set;}

    [Required(ErrorMessage =StringConstant.DUE_DATE_REQUIRED)]
    public DateOnly DueDate {get;set;}

    [Required(ErrorMessage =StringConstant.ASSIGNED_DATE_REQUIRED)]
    public DateOnly AssignedDate {get;set;}
    
    [Required(ErrorMessage =StringConstant.STATUS_REQUIRED)]
    public  TaskAssignmentStatus Status{get;set;}
    
    [MaxLength(50,ErrorMessage =StringConstant.REMARK_MAX_CHARACTER)]
    public  string Remarks{get;set;}=string.Empty;

}


public class TaskAssignmentUpdate
{
    public required TaskAssignmentStatus Status {get;set;}
}



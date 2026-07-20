
namespace trainee_management.Models.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using trainee_management.Constants;
using trainee_management.Enums;
using trainee_management.Models.DTOs;

public class TaskAssignment
{
    [Key]
    public int Id {get;set;}
    
    [Required(ErrorMessage =StringConstant.TRAINEE_ID_REQUIRED)]
    public  int TraineeId {get;set;}

    [DeleteBehavior(DeleteBehavior.Cascade)] 

    public  Trainee? Trainee {get;set;}

   [Required(ErrorMessage =StringConstant.LEARNING_TASK_ID_REQUIRED)]
    
    public  int LearningTaskId{get;set;}

    [DeleteBehavior(DeleteBehavior.Cascade)] 
    public  LearningTask? LearningTask {get;set;}
    
    [DeleteBehavior(DeleteBehavior.Cascade)] 
    public  Mentor? Mentor {get;set;}

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


    public TaskAssignment(TaskAssignmentRequest t)
    {
        TraineeId=t.TraineeId;
        MentorId=t.MentorId;
        LearningTaskId=t.LearningTaskId;
        DueDate=t.DueDate;
        AssignedDate=t.AssignedDate;
        Status=t.Status;
        Remarks=t.Remarks;
    }

    public TaskAssignment()
    {
        
    }

}
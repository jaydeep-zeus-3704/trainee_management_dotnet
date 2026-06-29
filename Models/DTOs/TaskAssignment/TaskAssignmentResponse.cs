
namespace trainee_management.Models.DTOs;

using System.ComponentModel.DataAnnotations;
using trainee_management.Enums;
using trainee_management.Models.Entities;

public class TaskAssignmentResponse
{
    public  int Id ;
    public  int TraineeId {get;set;}
    public  int MentorId{get;set;}

    public  int LearningTaskId{get;set;}

    public DateOnly DueDate {get;set;}

    public DateOnly AssignedDate {get;set;}
    
    public  TaskAssignmentStatus Status{get;set;}

    public  string Remarks{get;set;}=string.Empty;

    public TaskAssignmentResponse(TaskAssignment t)
    {
        Id=t.Id;
        MentorId=t.MentorId;
        LearningTaskId=t.LearningTaskId;
        TraineeId=t.TraineeId;
        DueDate=t.DueDate;
        AssignedDate=t.AssignedDate;
        Status=t.Status;
        Remarks=t.Remarks;
    }

    public TaskAssignmentResponse()
    {
        
    }


}


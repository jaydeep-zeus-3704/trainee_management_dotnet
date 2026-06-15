
namespace trainee_management.Models.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using trainee_management.Enums;
using trainee_management.Models.DTOs;

public class TaskAssignment
{
    [Key]
    public int Id {get;set;}
    
    [ForeignKey("Id")]
    [Required(ErrorMessage ="Trainee Id is required")]
    public  int TraineeId {get;set;}

    public  Trainee? Trainee {get;set;}



   [ForeignKey("Id")]
   [Required(ErrorMessage ="Learning Task id  is required")]
    
    public  int LearningTaskId{get;set;}


    public  LearningTask? LearningTask {get;set;}

    



    public  Mentor? Mentor {get;set;}

    [ForeignKey("Id")]
    [Required(ErrorMessage ="Mentor Id is required")]


    public  int MentorId{get;set;}

    [Required(ErrorMessage ="Due Date is required")]
    public DateOnly DueDate {get;set;}

    [Required(ErrorMessage ="Assigned Date is required")]
    public DateOnly AssignedDate {get;set;}
    
    [Required(ErrorMessage ="Task assignment status is required")]
    public  TaskAssignmentStatus Status{get;set;}

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
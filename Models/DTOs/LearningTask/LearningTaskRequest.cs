using System.ComponentModel.DataAnnotations;
using trainee_management.Constants;
using trainee_management.Enums;

namespace trainee_management.Models;

public class LearningTaskRequest
{

    [Required(ErrorMessage =StringConstant.TITLE_REQUIRED)]
    [Length(5,200,ErrorMessage =StringConstant.TITLE_CHARACTER_LIMIT)]
    public string Title{get;set;}=string.Empty;

    
    [Required(ErrorMessage =StringConstant.DESCRIPTION_REQUIRED)]
    [Length(5,200,ErrorMessage =StringConstant.DESCRIPTION_CHARACTER_LIMIT)]
    public string Description{get;set;}=string.Empty;
    
    [Required(ErrorMessage =StringConstant.EXPECTED_TECH_STACK_REQUIRED)]
    [Length(5,200,ErrorMessage =StringConstant.EXPECTED_TECH_STACK_CHARACTER_LIMIT)]

    public string ExpectedTechStack{get;set;}=string.Empty;

    [Required(ErrorMessage =StringConstant.DUE_DATE_REQUIRED)]
    public DateOnly DueDate {get;set;}

    [Required(ErrorMessage =StringConstant.STATUS_REQUIRED)]
    [EnumDataType(typeof(LearningTaskStatus),ErrorMessage =StringConstant.VALID_STATUS_REQUIRED)]
    public LearningTaskStatus Status {get;set;}


   public LearningTaskRequest(LearningTask learningTask)
    {
        Title=learningTask.Title;
        Description=learningTask.Description;
        ExpectedTechStack=learningTask.ExpectedTechStack;
        Status=learningTask.Status;
        DueDate=learningTask.DueDate;
    }

    public  LearningTaskRequest()
    {
        
    }
}
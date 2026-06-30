using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using trainee_management.Constants;

namespace trainee_management.Models.Entities;
public class Metadata
{
    public int Id {get;set;}

    [Required(ErrorMessage =StringConstant.CONTENT_TYPE_REQUIRED)]
    [Length(2,50,ErrorMessage =StringConstant.CONTENT_TYPE_CHARACTER_LIMIT)]
    public string ContentType{get;set;}=string.Empty;

    [Required(ErrorMessage =StringConstant.ORIGINAL_FILE_NAME_REQUIRED)]
    [Length(2,150,ErrorMessage =StringConstant.ORIGINAL_FILE_NAME_CHARACTER_LIMIT)]
    public string OriginalFileName{get;set;}=string.Empty; 
    
    [Required(ErrorMessage =StringConstant.GENERATED_STORAGE_NAME_REQUIRED)]
    [Length(2,50,ErrorMessage =StringConstant.GENERATED_STORAGE_NAME_CHARACTER_LIMIT)]
    public string GeneratedStorageName{get;set;}=string.Empty; 
    
    [ForeignKey("Id")]
    [Required(ErrorMessage =StringConstant.SUBMISSION_ID_REQUIRED)]
    public int SubmissionId {get;set;}
    public Submission? Submission {get;set;}


    [Required(ErrorMessage =StringConstant.FILE_SIZE_REQUIRED)]
    public int Size{get;set;}

    [Required(ErrorMessage =StringConstant.CHECK_SUM_REQUIRED)]
    [MaxLength(256,ErrorMessage =StringConstant.CHECK_SUM_MAX_CHARACTERS)]
    public string CheckSum {get;set;}=string.Empty;


    
    [Required(ErrorMessage =StringConstant.USER_ID_REQUIRED)]
    public int UploadedByUser {get;set;}
    
    public DateTime Timestamp {get;set;}
    

}
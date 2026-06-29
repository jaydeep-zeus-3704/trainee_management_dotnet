using System.ComponentModel.DataAnnotations;

namespace trainee_management.Models.Entities;
public class SubmissionFilesResponse
{
    public int Id {get;set;}

    [Required(ErrorMessage ="Content Type is required")]
    [Length(2,50,ErrorMessage ="Content type length must be between 2 and 50")]
    public string ContentType{get;set;}=string.Empty;

    [Required(ErrorMessage ="Original File name is required")]
    [Length(2,50,ErrorMessage ="Original File name  length must be between 2 and 50 characters")]
    public string OriginalFileName{get;set;}=string.Empty; 
    
    [Required(ErrorMessage ="Generated storage name is required")]
    [Length(2,50,ErrorMessage ="Generate Store name length must be between 2 and 50 characters")]
    public string GeneratedStorageName{get;set;}=string.Empty; 

    [Required(ErrorMessage ="Size is required")]
    public int Size{get;set;}

    [Required(ErrorMessage ="Checksum is required")]
    public string CheckSum {get;set;}=string.Empty;

    
    [Required(ErrorMessage ="User id is required")]
    public string UploadedByUser {get;set;}=string.Empty;
    
    public DateTime Timestamp {get;set;}


    public SubmissionFilesResponse(Metadata data,string username)
    {
        Id=data.Id;
        ContentType=data.ContentType;
        OriginalFileName=data.OriginalFileName;
        GeneratedStorageName=data.GeneratedStorageName;
        Size=data.Size;
        CheckSum=data.CheckSum;
        Timestamp=data.Timestamp;
        UploadedByUser=username;
    }

}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trainee_management.Models.Entities;
public class Metadata
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
    
    [ForeignKey("Id")]
    [Required(ErrorMessage ="Sumbission Id is required")]
    public int SubmissionId {get;set;}
    public Submission? Submission {get;set;}


    [Required(ErrorMessage ="Size is required")]
    public int Size{get;set;}

    [Required(ErrorMessage ="Checksum is required")]
    public string CheckSum {get;set;}=string.Empty;


    
    [Required(ErrorMessage ="User id is required")]
    public int UploadedByUser {get;set;}
    
    public DateTime Timestamp {get;set;}
    

}
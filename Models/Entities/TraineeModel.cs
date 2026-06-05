namespace trainee_management.Models;



public class Trainee
{
    public  int Id{get; set;}
    public required string FirstName {get;set;}    
    public required string LastName{get;set;} 
    public string? Email{get;set;}
    public List<string> TechStack{get;set;}=[];

    public String? Status{get;set;}
    public DateTime CreatedDate {get;set;}=DateTime.Now;
    public DateTime UpdatedDate {get;set;}
}
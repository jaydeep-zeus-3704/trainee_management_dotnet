using trainee_management.Enums;


namespace trainee_management.Models.DTOs
{

    public class TraineeResponse
    {
        public int Id{get;set;}
        public required string FirstName {get;set;}
        public required string LastName{get;set;} 
        public required string Email{get;set;}
        public required string TechStack{get;set;}
        public required StatusValues Status{get;set;}
        public DateTime CreatedDate {get;set;}
        
    }
}
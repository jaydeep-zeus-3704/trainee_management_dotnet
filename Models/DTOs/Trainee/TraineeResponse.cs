using trainee_management.Enums;
using trainee_management.Models.Entities;


namespace trainee_management.Models.DTOs
{

    public class TraineeResponse
    {
        public  int Id{get;set;}
        public  string FirstName {get;set;}
        public  string LastName{get;set;} 
        public  string Email{get;set;}
        public  string TechStack{get;set;}
        public  StatusValues Status{get;set;}

        
        public TraineeResponse(Trainee trainee)
        {
            Id=trainee.Id;
            FirstName=trainee.FirstName;
            LastName=trainee.LastName;
            TechStack=trainee.TechStack;
            Email=trainee.Email;
            Status=trainee.Status;
        }

        
    }
}
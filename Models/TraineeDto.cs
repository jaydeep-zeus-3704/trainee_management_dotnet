namespace trainee_management.Models
{
    public class TraineeDto
    {
        public  int Id{get; set;}
        public required string FirstName {get;set;}    
        public required string LastName{get;set;} 
        public string? Email{get;set;}
        public List<string> TechStack{get;set;}=[];
    }
}
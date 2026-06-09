using System.Text.RegularExpressions;
using trainee_management.Models.Entities;

namespace trainee_management.Validator
{
    public class TraineeValidator {
         private readonly Trainee _trainee; 

        public TraineeValidator(Trainee trainee)
        {
            _trainee=trainee;
        }

        public bool Validate()
        {
            if (_trainee == null)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_trainee.FirstName))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_trainee.LastName))
            {
                return false;
            }
            if (IsValidEmail(_trainee.Email))
            {
                return false;
            }
            return true;
        }
            
        public static bool IsValidEmail(string email)
	    {
		    string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (string.IsNullOrEmpty(email))
                return false;
            
            Regex regex = new Regex(emailPattern);
            return regex.IsMatch(email);
	    }       
    }    
}


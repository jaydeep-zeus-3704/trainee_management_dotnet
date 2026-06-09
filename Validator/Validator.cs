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
            if (!validateName(_trainee.FirstName))
            {
                return false;
            }
            if (!validateName(_trainee.LastName))
            {
                return false;
            }
            if (!IsValidEmail(_trainee.Email))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_trainee.TechStack))
            {
                return false;
            }
            return true;
        }


            
        public static bool IsValidEmail(string email)
	    {
		    string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (string.IsNullOrWhiteSpace(email))
                return false;
            
            Regex regex = new Regex(emailPattern);
            return regex.IsMatch(email);
	    }       

        public static bool validateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            string pattern = @"^[\p{L}][\p{L}'\- ]{0,49}$";
            Regex regex=new Regex(pattern);
            return regex.IsMatch(name);
        }
    }    
}


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
            if (string.IsNullOrWhiteSpace(_trainee.Email))
            {
                return false;
            }

            if (IsValidEmail(_trainee.Email))
            {
                return false;
            }
            return true;

        }
            
         private bool IsValidEmail(string email)
        {
            try
            {
                var addr=new System.Net.Mail.MailAddress(email);
                return addr.Address==email;
            }
            catch 
            {
                
                return false;
            }
        }   
            
        }

         
    }


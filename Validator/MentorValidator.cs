using System.Text.RegularExpressions;
using trainee_management.Models.Entities;

public class MentorValidator
{
    private readonly Mentor _mentor; 

    public MentorValidator(Mentor mentor)
    {
        _mentor=mentor;
    }

    public bool Validate()
        {
            if (_mentor == null)
            {
                return false;
            }
            if (!validateName(_mentor.FirstName))
            {
                return false;
            }
            if (!validateName(_mentor.LastName))
            {
                return false;
            }
            if (!IsValidEmail(_mentor.Email))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(_mentor.Expertise))
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
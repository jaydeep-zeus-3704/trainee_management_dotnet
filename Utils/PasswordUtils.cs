using Microsoft.AspNetCore.Identity;

namespace trainee_management.Utils;

public class PasswordUtils
{
    public static string hashPassword(string username,string password)
    {
        PasswordHasher<string> hasher=new PasswordHasher<string>();
        string hashedPassword=hasher.HashPassword(username,password);
        return hashedPassword;
    }

    public static bool verifyPassword(string username,string password,string hashedPassword)
    {
        PasswordHasher<string> hasher=new PasswordHasher<string>();
        PasswordVerificationResult result=hasher.VerifyHashedPassword(username,hashedPassword,password);
        return result == PasswordVerificationResult.Success || result==PasswordVerificationResult.SuccessRehashNeeded;
    }

    

}
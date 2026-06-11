using System.ComponentModel.DataAnnotations;
using trainee_management.Enums;
namespace trainee_management.Models.DTOs;


public class UserResponse
{
    public int Id {get;set;}
    public  required  string  Username{get;set;}
    
    public required UserRole role {get;set;}
}

public class LoginResponse
{
    public required string token {get;set;}
    public required string expiresIn {get;set;}
    public required UserResponse user {get;set;}
}
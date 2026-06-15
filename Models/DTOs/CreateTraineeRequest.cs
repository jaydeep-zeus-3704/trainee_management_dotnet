using System.ComponentModel.DataAnnotations;
using trainee_management.Enums;

namespace trainee_management.Models.DTOs
{

    
    public class CreateTraineeRequest
    {
        [MinLength(2, ErrorMessage = "First name should contain minimum 2 characters")]
        [MaxLength(50, ErrorMessage = "First name should contain maximum 50 characters")]
        [Required(ErrorMessage = "FirstName is required")]
        public required string FirstName { get; set; }



        [MinLength(2, ErrorMessage = "Last name should contain minimum 2 characters")]
        [MaxLength(50, ErrorMessage = "Last name should contain maximum 50 characters")]
        [Required(ErrorMessage = "LastName is required")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Techstack is required")]
        public required string TechStack { get; set; }


        [Required(ErrorMessage = "Status is required ")]
        public required StatusValues Status { get; set; }

    }
}
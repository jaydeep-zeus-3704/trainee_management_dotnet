using System.ComponentModel.DataAnnotations;

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
        public string? Email { get; set; }

        [MinLength(1, ErrorMessage = "At Least one skill is required")]
        public required string TechStack { get; set; }


        [AllowedValues("Active", "Inactive", "Completed","active","inactive","completed")]
        [Required(ErrorMessage = "Status is required ")]
        public required String Status { get; set; }

    }
}
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BCPT.ABSTACTION
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "The {0} is required")]
        [RegularExpression(@"^[a-zA-Z0-9_-]{3,16}$", ErrorMessage = "The {0} must be 3-16 characters long and can contain letters, numbers, '_', and '-'")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [EmailAddress()]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "The {0} must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [CustomRoleValidation]
        public string Role { get; set; }
    }
}

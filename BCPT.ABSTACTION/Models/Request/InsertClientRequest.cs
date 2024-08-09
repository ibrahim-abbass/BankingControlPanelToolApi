using System.ComponentModel.DataAnnotations;

namespace BCPT.ABSTACTION
{
    public class InsertClientRequest
    {
        [Required]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "The {0} must be between {2} and {1}")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "The {0} must be between {2} and {1}")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress()]
        public string Email { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "The {0} must be exact {1}")]
        public string PersonalId { get; set; }

        [CustomPhoneNumberValidation]
        public string? MobileNumber { get; set; }

        [Required]
        [CustomGenderValidation(ErrorMessage = "The Sex must be male or female")]
        public string Sex { get; set; }

        public string? ProfilePhoto { get; set; }

        public AddressDto? Address { get; set; }

        [EnsureMinimumElements(1, ErrorMessage = "At least one account is required")]
        public List<AccountDto> Accounts { get; set; }
    }
}
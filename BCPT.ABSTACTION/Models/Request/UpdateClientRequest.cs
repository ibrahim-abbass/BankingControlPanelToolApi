using System.ComponentModel.DataAnnotations;

namespace BCPT.ABSTACTION
{
    public class UpdateClientRequest
    {
        [StringLength(60, MinimumLength = 1, ErrorMessage = "The {0} must be between {2} and {1}")]
        public string? FirstName { get; set; }

        [StringLength(60, MinimumLength = 1, ErrorMessage = "The {0} must be between {2} and {1}")]
        public string? LastName { get; set; }

        [EmailAddress()]
        public string? Email { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "The {0} must be exact {1}")]
        public string? PersonalId { get; set; }

        [CustomPhoneNumberValidation]
        public string? MobileNumber { get; set; }

        [CustomGenderValidation(ErrorMessage = "The sex must be male or female")]
        public string? Sex { get; set; }

        public string? ProfilePhoto { get; set; }

        public AddressDto? Address { get; set; }

        public List<AccountDto>? Accounts { get; set; }
    }
}

using BCPT.ABSTACTION.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BCPT.ABSTACTION
{
    public class Client
    {
        [Required(ErrorMessage = "The {0} is required")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "The {0} must be between {2} and {1}")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "The {0} must be between {2} and {1}")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [EmailAddress()]
        public string Email { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "The {0} must be exact {1}")]
        public string PersonalId { get; set; }

        [CustomPhoneNumberValidation]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "The sex is required")]
        [CustomGenderValidation]
        public string Sex { get; set; }

        public string? ProfilePhoto { get; set; }

        public Address? Address { get; set; }

        [EnsureMinimumElements(1, ErrorMessage = "At least one account is required")]
        public List<Account> Accounts { get; set; }
    }
}

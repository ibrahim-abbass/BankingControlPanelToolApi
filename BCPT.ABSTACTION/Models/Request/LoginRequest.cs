using System.ComponentModel.DataAnnotations;

namespace BCPT.ABSTACTION
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "The {0} is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public string? Password { get; set; }
    }
}

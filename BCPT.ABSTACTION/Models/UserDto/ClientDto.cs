using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCPT.ABSTACTION
{
    public class ClientDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PersonalId { get; set; }

        public string MobileNumber { get; set; }

        public string Sex { get; set; }

        public string ProfilePhoto { get; set; }

        public AddressDto Address { get; set; }

        public List<AccountDto> Accounts { get; set; }
    }
}

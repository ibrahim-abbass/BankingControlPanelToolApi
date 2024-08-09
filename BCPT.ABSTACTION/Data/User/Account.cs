using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BCPT.ABSTACTION
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string AccountNumber { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }

        public Guid ClientId { get; set; }
    }
}

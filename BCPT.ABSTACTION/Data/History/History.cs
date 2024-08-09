using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCPT.ABSTACTION
{
    public class History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string SearchProperty { get; set; }
        public string SearchParameter { get; set; }
        public string SearchResult { get; set; }
        public DateTime SearchDate { get; set; }
    }
}

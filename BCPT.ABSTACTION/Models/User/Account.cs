using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCPT.ABSTACTION
{
    public class Account
    {
        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }
    }
}

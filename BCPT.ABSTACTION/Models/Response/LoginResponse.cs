using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCPT.ABSTACTION
{
    public class LoginResponse : Response
    {
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }
    }
}

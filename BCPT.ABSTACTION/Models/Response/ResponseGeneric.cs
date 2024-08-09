using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCPT.ABSTACTION
{
    public class Response<T> : Response
    {
        public T Data { get; set; }
    }
}

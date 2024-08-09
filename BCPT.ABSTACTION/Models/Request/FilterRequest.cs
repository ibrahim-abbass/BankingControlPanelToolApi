using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCPT.ABSTACTION
{
    public class FilterRequest
    {
        public string? FilterBy { get; set; }
        public string FilterValue { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}

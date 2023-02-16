using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiephashop.Ultility.Common
{
    public class RequestSearch
    {
        public int PageNumber { get; set; }
        public string? SortPrice { get; set; }
        public IEnumerable<string>? Categories { get; set; }
        public double? FromRange { get; set; } = 0;
        public double ToRange { get; set; } = 0;
        public string? SearchName { get; set; }
    }
}

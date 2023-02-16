using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiephashop.Data.Entities
{
    public class CategoryDetail : BaseEntity
    {
        public string? Title { get; set; }
        public long Order { get; set; } = 0;
        public bool Status { get; set; } = true;
        public bool IsBold { get; set; } = false;
        public string CategoryCode { get; set; }
        public Category Category { get; set; }
    }
}

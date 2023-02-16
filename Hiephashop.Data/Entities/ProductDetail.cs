using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiephashop.Data.Entities
{
    public class ProductDetail : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Decription { get; set; }
        public long Order { get; set; }
        public bool Status { get; set; }
        public bool IsBold { get; set; }
        public string ProductCode { get; set; }
        public Product Product { get; set; }
    }
}

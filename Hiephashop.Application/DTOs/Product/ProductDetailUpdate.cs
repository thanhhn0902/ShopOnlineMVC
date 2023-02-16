using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiephashop.Application.DTOs.Product
{
    public class ProductDetailUpdate
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public long Order { get; set; } = 0;
        public bool Status { get; set; } = true;
        public bool IsBold { get; set; } = false;
    }
}

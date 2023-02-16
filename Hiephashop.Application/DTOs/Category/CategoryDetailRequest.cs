using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiephashop.Application.DTOs.Category
{
    public class CategoryDetailRequest
    {
        public string? Title { get; set; }
        public long Order { get; set; } = 0;
        public bool Status { get; set; } = true;
        public bool IsBold { get; set; } = false;
    }
}

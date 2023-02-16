using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiephashop.Application.DTOs.Category
{
    public class CategoryDropDown
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentCode { get; set; }
        public string? Thumnail { get; set; }
        public IEnumerable<CategoryDropDown> ListChild { get; set; }
    }
}

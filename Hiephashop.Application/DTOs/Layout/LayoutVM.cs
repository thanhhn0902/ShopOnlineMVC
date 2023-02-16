using Hiephashop.Application.DTOs.Category;
using Hiephashop.Application.DTOs.Product;
using Hiephashop.Ultility.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiephashop.Application.DTOs.Layout
{
    public class LayoutVM
    {
        public IEnumerable<CategoryVM> categories { get; set; } = new List<CategoryVM>();
        public codeMenu isMenu { get; set; } = codeMenu.Home;
        public PaginatedList<ProductVM>? listProduct { get; set; } = new PaginatedList<ProductVM>();
        public double? MaxPrice { get; set; }
        public int totalPages { get; set; }
        public int pageIndex { get; set; }
        public bool hasNextPage { get; set; }
        public bool hasPreviosPage { get; set; }
    }

    public enum codeMenu
    {
        Home = 1,
        Product = 2,
        Contact = 3
    }
}

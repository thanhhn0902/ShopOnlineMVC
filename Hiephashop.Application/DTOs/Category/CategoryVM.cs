using Microsoft.AspNetCore.Http;

namespace Hiephashop.Application.DTOs.Category
{
    public class CategoryVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? ParentCode { get; set; }
        public string? Thumnail { get; set; }
        public string? ParentName { get; set; }
        public IEnumerable<CategoryVM> ListChild { get; set; }
    }
}

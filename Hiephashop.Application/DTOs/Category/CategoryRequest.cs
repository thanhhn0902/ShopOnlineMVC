using Microsoft.AspNetCore.Http;

namespace Hiephashop.Application.DTOs.Category
{
    public class CategoryRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? ParentCode { get; set; }
        public string? Thumnail { get; set; }
        public bool Status { get; set; }
        public IEnumerable<IFormFile> Images { get; set; } = new List<IFormFile>();
        public string? ParentName { get; set; }
        public string ArrayJson { get; set; }
        public IEnumerable<CategoryDetailRequest> ListDetail { get; set; }
    }
}

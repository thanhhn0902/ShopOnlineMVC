using Microsoft.AspNetCore.Http;

namespace Hiephashop.Application.DTOs.Product
{
    public class ProductRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Summary { get; set; }
        public string? Decription { get; set; }
        public double? Price { get; set; }
        public double? Sale { get; set; }
        public string? Thumnail { get; set; }
        public string CategoryCode { get; set; }
        public string SupplierCode { get; set; }
        public bool Status { get; set; }
        public IEnumerable<IFormFile> Images { get; set; } = new List<IFormFile>();
        public string ArrayJson { get; set; }
        public IEnumerable<ProductDetailRequest> ListDetail { get; set; }
    }
}

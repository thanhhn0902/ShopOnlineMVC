using Microsoft.AspNetCore.Http;

namespace Hiephashop.Application.DTOs.Product
{
    public class ProductVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Summary { get; set; }
        public string? Decription { get; set; }
        public double? Price { get; set; }
        public double? Sale { get; set; }
        public bool Status { get; set; } = true;
        public string? CategoryCode { get; set; }
        public string? SupplierCode { get; set; }
        public string? Thumnail { get; set; }
        public string PriceStr { get; set; }
        public string SaleStr { get; set; }

    }
}

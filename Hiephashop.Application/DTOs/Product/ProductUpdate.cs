using Hiephashop.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hiephashop.Application.DTOs.Product
{
    public class ProductUpdate
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Summary { get; set; }
        public string? Decription { get; set; }
        public double? Price { get; set; }
        public double? Sale { get; set; }
        public bool Status { get; set; } = true;
        public string CategoryCode { get; set; }
        public string SupplierCode { get; set; }
        public IEnumerable<Files> InfoImage { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
        public IEnumerable<Guid> ListFileDel { get; set; } = new List<Guid>();
        public IEnumerable<ProductDetailUpdate> ListDetail { get; set; }
        public IEnumerable<ProductDetailRequest> ListDetailInsert { get; set; }
        public string? ArrayJsonInsert { get; set; }
        public string? ArrayJsonFileDel { get; set; }
        public string? PriceStr { get; set; }
        public string? SaleStr { get; set; }
    }
}

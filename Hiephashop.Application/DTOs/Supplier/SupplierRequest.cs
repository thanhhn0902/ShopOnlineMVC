using Hiephashop.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hiephashop.Application.DTOs.Supplier
{
    public class SupplierRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Decription { get; set; }
        public string? Logo { get; set; }
        public IEnumerable<IFormFile> Images { get; set; } = new List<IFormFile>();
    }
}

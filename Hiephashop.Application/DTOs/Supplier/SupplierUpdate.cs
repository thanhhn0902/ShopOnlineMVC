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
    public class SupplierUpdate
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Decription { get; set; }
        public Guid? Logo { get; set; }
        public Files InfoLogo { get; set; }
        public IFormFile Images { get; set; }
        public IEnumerable<Guid> ListFileDel { get; set; } = new List<Guid>();
    }
}

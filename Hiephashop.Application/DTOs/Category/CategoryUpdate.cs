using Hiephashop.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hiephashop.Application.DTOs.Category
{
    public class CategoryUpdate
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? ParentCode { get; set; }
        public Guid Thumnail { get; set; }
        public bool Status { get; set; } = true;
        public Files InfoLogo { get; set; }
        public IFormFile Images { get; set; }
        public IEnumerable<Guid> ListFileDel { get; set; } = new List<Guid>();
        public IEnumerable<CategoryDetailUpdate> ListDetail { get; set; }
        public IEnumerable<CategoryDetailUpdate> ListDetailUpdate { get; set; }
        public IEnumerable<CategoryDetailRequest> ListDetailInsert { get; set; }
        public IEnumerable<Guid> ListDetailDel { get; set; }
        public string? ArrayJsonInsert { get; set; }
        public string? ArrayJsonUpdate { get; set; }
        public string? ArrayJsonDel { get; set; }
    }
}

using Hiephashop.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hiephashop.Application.DTOs.User
{
    public class UserUpdate
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Guid Avatar { get; set; }
        public bool Status { get; set; } = true;
        public int Role { get; set; } = 0;
        public Files InfoLogo { get; set; }
        public IFormFile Images { get; set; }
        public IEnumerable<Guid> ListFileDel { get; set; } = new List<Guid>();
    }
}

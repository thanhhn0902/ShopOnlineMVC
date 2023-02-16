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
    public class UserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public bool Status { get; set; } = true;
        public int Role { get; set; } = 1;
        public IFormFile Images { get; set; }
    }
}

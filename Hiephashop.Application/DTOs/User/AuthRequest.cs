using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiephashop.Application.DTOs.User
{
    public class AuthRequest
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}

using Hiephashop.Application.DTOs.User;
using Hiephashop.Data;
using Hiephashop.Ultility.Common;
using Hiephashop.Ultility.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Xml.Linq;

namespace Hiephashop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly DbContextOptions<ShopDbContext> _options;

        public AuthController(DbContextOptions<ShopDbContext> options)
        {
            _options = options;
        }

        public IActionResult Index()
        {
            return Login();
        }

        public IActionResult Login()
        {
            HttpContext.Session.Remove(SessionName.SessionUser);
            return View();
        }

        [HttpPost]
        public IActionResult Login(AuthRequest request)
        {
            if (request != null && !string.IsNullOrEmpty(request.UserName) && !string.IsNullOrEmpty(request.PassWord))
            {
                using (var db = new ShopDbContext(_options))
                {
                    var user = db.Users.SingleOrDefault(x => x.UserName.Equals(request.UserName) && x.Password.Equals(CommonFunction.MD5Hash(request.PassWord)));
                    if (user != null)
                    {
                        var file = db.Files.FirstOrDefault(t => t.Id.Equals(user.Avatar));
                        var info = new UserVM
                        {
                            UserName = user.UserName,
                            Name = user.Name,
                            Phone = user.Phone,
                            Email = user.Email,
                            Address = user.Address,
                            Avatar = file == null ? "" : file.Url,
                            Status = user.Status,
                            Role = user.Role
                        };
                        HttpContext.Session.SetString(SessionName.SessionUser, JsonConvert.SerializeObject(info));
                        return Redirect("/Admin");
                    }
                }
            }
            return View();
        }
    }
}

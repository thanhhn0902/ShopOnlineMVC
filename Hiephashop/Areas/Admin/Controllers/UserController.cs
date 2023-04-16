using Hiephashop.Application.DTOs.User;
using Hiephashop.Application.Service.Users;
using Hiephashop.Ultility.Common;
using Hiephashop.Ultility.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hiephashop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        // GET: SuppilerController
        public ActionResult Index()
        {
            var data = _service.GetAll();
            return View(data);
        }

        // GET: SuppilerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SuppilerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuppilerController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("/Admin/User/Create")]
        public ActionResult Create(UserRequest request)
        {
            try
            {
                if (request != null)
                {
                    var status = _service.Create(request);
                    var check = CommonFunction.CheckStatusCRUD(status);
                    if (check != string.Empty)
                    {
                        ViewData["MessageError"] = check;
                        return View();
                    }
                    return Redirect("/Admin/User");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: SuppilerController/Edit/5
        [Route("/Admin/User/Edit/{code}")]
        public ActionResult Edit(string code)
        {
            var obj = _service.GetOne(code);
            if (obj != null)
            {
                return View(obj);
            }
            return Redirect("/Admin/User");
        }

        // POST: SuppilerController/Edit/5
        [HttpPost]
        [Route("/Admin/User/Edit")]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(UserUpdate request)
        {
            try
            {
                if (request != null)
                {
                    var status = _service.Edit(request);
                    var check = CommonFunction.CheckStatusCRUD(status);
                    if (check != string.Empty)
                    {
                        ViewData["MessageError"] = check;
                        return View();
                    }
                    return Redirect("/Admin/User");
                }
                return Edit(request?.UserName ?? "");
            }
            catch
            {
                return View();
            }
        }

        // GET: SuppilerController/Delete/5
        [Route("/Admin/User/Delete/{code}")]
        public ActionResult Delete(string code)
        {
            ViewData["code"] = code;
            return View();
        }

        // POST: SuppilerController/Delete/5
        [Route("/Admin/User/PostDelete/{code}")]
        public ActionResult PostDelete(string code)
        {
            try
            {
                if (code != string.Empty)
                {
                    var status = _service.Delete(code);
                    var check = CommonFunction.CheckStatusCRUD(status);
                    if (check != string.Empty)
                    {
                        ViewData["MessageError"] = check;
                        return View();
                    }
                    return Redirect("/Admin/User");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}

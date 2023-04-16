using Hiephashop.Application.DTOs.Supplier;
using Hiephashop.Application.Service.Suppliers;
using Hiephashop.Ultility.Common;
using Hiephashop.Ultility.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hiephashop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupplierController : Controller
    {
        ISupplierService _service;
        public SupplierController(ISupplierService service)
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
        [Route("/Admin/Supplier/Create")]
        public ActionResult Create([Bind("Code, Name, Decription, Images")] SupplierRequest request)
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
                    return Redirect("/Admin/Supplier");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: SuppilerController/Edit/5
        [Route("/Admin/Supplier/Edit/{code}")]
        public ActionResult Edit(string code)
        {
            var obj = _service.GetOne(code);
            if (obj != null)
            {
                return View(obj);
            }
            return Redirect("/Admin/Supplier");
        }

        // POST: SuppilerController/Edit/5
        [HttpPost]
        [Route("/Admin/Supplier/Edit")]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierUpdate request)
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
                   return Redirect("/Admin/Supplier");
                }
                return Edit(request.Code);
            }
            catch
            {
                return View();
            }
        }

        // GET: SuppilerController/Delete/5
        [Route("/Admin/Supplier/Delete/{code}")]
        public ActionResult Delete(string code)
        {
            ViewData["code"] = code;
            return View();
        }

        // POST: SuppilerController/Delete/5
        [Route("/Admin/Supplier/PostDelete/{code}")]
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
                   return Redirect("/Admin/Supplier");
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

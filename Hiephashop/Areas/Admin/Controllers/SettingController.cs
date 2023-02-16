using Hiephashop.Application.DTOs.Setting;
using Hiephashop.Application.Service.Setting;
using Hiephashop.Ultility.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hiephashop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        ISettingService _service;

        public SettingController(ISettingService service)
        {
            _service = service;
        }

        // GET: SuppilerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SuppilerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SuppilerController/Edit/5
        public ActionResult Edit()
        {
            var obj = _service.GetOne();
            if (obj != null)
            {
                return View(obj);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: SuppilerController/Edit/5
        [HttpPost]
        [Route("/Admin/Setting/Edit")]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(SettingUpdate request)
        {
            try
            {
                if (request != null)
                {
                    var listFileDel = new List<Guid>();
                    if (!string.IsNullOrEmpty(request.ArrayJsonFileDel))
                    {
                        foreach (var item in request.ArrayJsonFileDel?.Split(","))
                        {
                            listFileDel.Add(Guid.Parse(item));
                        }
                        request.ListFileDel = listFileDel;
                    }

                    var status = _service.Edit(request);
                    var check = CommonFunction.CheckStatusCRUD(status);
                    if (check != string.Empty)
                    {
                        ViewData["MessageError"] = check;
                        return Edit();
                    }
                    return Redirect("/Admin");
                }
                return Edit();
            }
            catch
            {
                return Edit();
            }
        }
    }
}

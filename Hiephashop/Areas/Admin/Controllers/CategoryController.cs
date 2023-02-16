using Hiephashop.Application.DTOs.Category;
using Hiephashop.Application.Service.Categories;
using Hiephashop.Ultility.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hiephashop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        ICategoryService _service;
        public CategoryController(ICategoryService service)
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
            ViewBag.DataDropDown = _service.GetAll().Where(x => string.IsNullOrEmpty(x.ParentCode)).Select(t => new CategoryDropDown { Code = t.Code, Name = t.Name });
            return View();
        }

        // POST: SuppilerController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("/Admin/Category/Create")]
        public ActionResult Create([Bind("Code, Name, ParentCode, Images, ArrayJson")] CategoryRequest request)
        {
            try
            {
                if (request != null)
                {
                    if (!string.IsNullOrEmpty(request.ArrayJson))
                    {
                        request.ListDetail = JsonConvert.DeserializeObject<IEnumerable<CategoryDetailRequest>>(request.ArrayJson);
                    }

                    var status = _service.Create(request);
                    var check = CommonFunction.CheckStatusCRUD(status);
                    if (check != string.Empty)
                    {
                        ViewData["MessageError"] = check;
                        return View();
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: SuppilerController/Edit/5
        [Route("/Admin/Category/Edit/{code}")]
        public ActionResult Edit(string code)
        {
            var obj = _service.GetOne(code);
            ViewBag.DataDropDown = _service.GetAll().Where(x => string.IsNullOrEmpty(x.ParentCode)).Select(t => new CategoryDropDown { Code = t.Code, Name = t.Name });
            if (obj != null)
            {
                return View(obj);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: SuppilerController/Edit/5
        [HttpPost]
        [Route("/Admin/Category/Edit")]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryUpdate request)
        {
            try
            {
                
                if (request != null)
                {
                    request.ListDetailDel = string.IsNullOrEmpty(request.ArrayJsonDel) ? null : JsonConvert.DeserializeObject<IEnumerable<Guid>>(request.ArrayJsonDel);
                    request.ListDetailUpdate = request.ArrayJsonUpdate == null ? null : JsonConvert.DeserializeObject<IEnumerable<CategoryDetailUpdate>>(request.ArrayJsonUpdate);
                    request.ListDetailInsert = request.ArrayJsonInsert == null ? null : JsonConvert.DeserializeObject<IEnumerable<CategoryDetailRequest>>(request.ArrayJsonInsert);
                    var status = _service.Edit(request);
                    var check = CommonFunction.CheckStatusCRUD(status);
                    if (check != string.Empty)
                    {
                        ViewData["MessageError"] = check;
                        return View();
                    }
                    return RedirectToAction(nameof(Index));
                }
                return Edit(request.Code);
            }
            catch
            {
                return Edit(request.Code);
            }
        }

        // GET: SuppilerController/Delete/5
        [Route("/Admin/Category/Delete/{code}")]
        public ActionResult Delete(string code)
        {
            ViewData["code"] = code;
            return View();
        }

        // POST: SuppilerController/Delete/5
        [Route("/Admin/Category/PostDelete/{code}")]
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
                    return RedirectToAction(nameof(Index));
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

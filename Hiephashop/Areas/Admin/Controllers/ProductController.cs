using Hiephashop.Application.DTOs.Category;
using Hiephashop.Application.DTOs.Product;
using Hiephashop.Application.DTOs.Supplier;
using Hiephashop.Application.Service.Categories;
using Hiephashop.Application.Service.products;
using Hiephashop.Application.Service.Suppliers;
using Hiephashop.Ultility.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hiephashop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        IProductService _service;
        ICategoryService _cateService;
        ISupplierService _supplierService;
        public ProductController(IProductService service, ICategoryService cateService, ISupplierService supplierService)
        {
            _service = service;
            _cateService = cateService;
            _supplierService = supplierService;
        }

        // GET: SuppilerController
        public ActionResult Index()
        {
            ViewBag.DataDropDown = _cateService.GetTree();
            var data = _service.GetAll();
            return View(data);
        }

        [HttpGet]
        [Route("/Admin/Product/Index/{cateCode}")]
        public ActionResult Index(string cateCode)
        {
            ViewBag.DataDropDown = _cateService.GetTree();
            var data = _service.GetAll();
            if (!string.IsNullOrEmpty(cateCode))
            {
                data = _service.GetAll().Where(x => cateCode.Equals(x.CategoryCode));
            }
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
            ViewBag.DataDropDown = _cateService.GetTree();
            return View();
        }

        [HttpGet]
        [Route("/Admin/Product/Create/{cateCode}")]
        public ActionResult Create(string cateCode)
        {
            ViewBag.CodeCategory = cateCode;
            ViewBag.SupplierDropDown = _supplierService.GetAll().Select(x => new SupplierDropDown
            {
                Code = x.Code,
                Name = x.Name,
                Decription = x.Decription,
                Logo = x.Logo
            });
            var data = _cateService.GetOne(cateCode);

            if (data != null)
            {
                ViewBag.TemplateTable = data.ListDetail;
            }
            return View();
        }

        // POST: SuppilerController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("/Admin/Product/Create")]
        public ActionResult Create([Bind("Code, Name, Summary, Decription, Price, Sale, CategoryCode, SupplierCode, Images, ArrayJson")] ProductRequest request)
        {
            try
            {
                if (request != null)
                {
                    request.ListDetail = string.IsNullOrEmpty(request.ArrayJson) ? null : JsonConvert.DeserializeObject<IEnumerable<ProductDetailRequest>>(request.ArrayJson);

                    var status = _service.Create(request);
                    var check = CommonFunction.CheckStatusCRUD(status);
                    if (check != string.Empty)
                    {
                        ViewData["MessageError"] = check;
                        return View();
                    }
                    return Redirect("/Admin/Product");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: SuppilerController/Edit/5
        [Route("/Admin/Product/Edit/{code}/{categoryCode?}")]
        public ActionResult Edit(string code, string? categoryCode)
        {
            ViewBag.SupplierDropDown = _supplierService.GetAll().Select(x => new SupplierDropDown
            {
                Code = x.Code,
                Name = x.Name,
                Decription = x.Decription,
                Logo = x.Logo
            });

            ViewBag.CategoryDropDown = _cateService.GetTree();

            var obj = _service.GetOne(code, categoryCode);
            if (obj != null)
            {
                return View(obj);
            }
            return Redirect("/Admin/Product");
        }

        // POST: SuppilerController/Edit/5
        [HttpPost]
        [Route("/Admin/Product/Edit")]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(ProductUpdate request)
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

                    request.ListDetailInsert = string.IsNullOrEmpty(request.ArrayJsonInsert) ? null : JsonConvert.DeserializeObject<IEnumerable<ProductDetailRequest>>(request.ArrayJsonInsert);
                    var status = _service.Edit(request);
                    var check = CommonFunction.CheckStatusCRUD(status);
                    if (check != string.Empty)
                    {
                        ViewData["MessageError"] = check;
                        return View();
                    }
                    return Redirect("/Admin/Product");
                }
                return Edit(request.Code, null);
            }
            catch
            {
                return Edit(request.Code, null);
            }
        }

        // GET: SuppilerController/Delete/5
        [Route("/Admin/Product/Delete/{code}")]
        public ActionResult Delete(string code)
        {
            ViewData["code"] = code;
            return View();
        }

        // POST: SuppilerController/Delete/5
        [Route("/Admin/Product/PostDelete/{code}")]
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
                    return Redirect("/Admin/Product");
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

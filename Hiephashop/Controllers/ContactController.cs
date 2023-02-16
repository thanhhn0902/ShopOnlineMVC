using Hiephashop.Application.DTOs.Layout;
using Hiephashop.Application.Service.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Hiephashop.Web.Controllers
{
    public class ContactController : Controller
    {
        private ICategoryService _categoryService;
        public ContactController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            ViewBag.ListCategory = _categoryService.GetTree();
            return View(new LayoutVM { categories = ViewBag.ListCategory, isMenu = codeMenu.Contact });
        }
    }
}

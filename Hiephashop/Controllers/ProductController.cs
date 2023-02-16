using Hiephashop.Application.DTOs.Layout;
using Hiephashop.Application.DTOs.Product;
using Hiephashop.Application.Service.Categories;
using Hiephashop.Application.Service.products;
using Hiephashop.Ultility.Common;
using Hiephashop.Ultility.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Hiephashop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public ProductController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [HttpGet]
        [Route("/Product/Index/{categoryCode?}/{searhName?}")]
        public IActionResult Index(string? categoryCode, string? searhName)
        {
            var ListCategory = _categoryService.GetTree();

            var listProduct = _productService.GetAll().OrderBy(o => o.Sale).AsQueryable();

            var maxPrirce = listProduct.Max(x => x.Sale);

            if (!string.IsNullOrEmpty(categoryCode) && !categoryCode.Equals("undefined"))
            {
                listProduct = listProduct.Where(x => categoryCode.Equals(x.CategoryCode));
            }

            if (!string.IsNullOrEmpty(searhName) && !searhName.Equals("undefined"))
            {
                listProduct = listProduct.Where(x => x.Name.Contains(searhName, StringComparison.OrdinalIgnoreCase));
            }

            var pageProduct = PaginatedList<ProductVM>.CreateAsync(listProduct, 1, (int)ConstantsPaging.PageSize);
            return View(new LayoutVM { categories = ListCategory, isMenu = codeMenu.Product, listProduct = pageProduct, MaxPrice = maxPrirce });
        }

        [HttpPost]
        public IActionResult Index(RequestSearch request)
        {
            var ListCategory = _categoryService.GetTree();
            var pageProduct = new PaginatedList<ProductVM>();

            var listProduct = _productService.GetAll();

            var maxPrirce = listProduct.Max(x => x.Sale);

            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.SearchName))
                {
                    listProduct = listProduct.Where(x => x.Name.Contains(request.SearchName, StringComparison.OrdinalIgnoreCase));
                }

                if (request.Categories?.Count() > 0)
                {
                    listProduct = listProduct.Where(x => request.Categories.Any(t => t.Equals(x.CategoryCode)));
                }

                if (request?.FromRange >= 0 && request.ToRange > 0)
                {
                    listProduct = listProduct.Where(x => x.Sale >= request.FromRange && x.Sale <= request.ToRange);
                }

                if (!string.IsNullOrEmpty(request?.SortPrice))
                {
                    var sort = (int)ConstantsPaging.AscPrice;
                    if (int.Parse(request.SortPrice) == sort)
                    {
                        listProduct = listProduct.OrderBy(o => o.Sale);
                    }
                    else
                    {
                        listProduct = listProduct.OrderByDescending(o => o.Sale);
                    }
                }
            }


            if (listProduct != null && listProduct.Count() > 0)
            {
                pageProduct = PaginatedList<ProductVM>.CreateAsync(listProduct.AsQueryable(), request?.PageNumber ?? 1, (int)ConstantsPaging.PageSize);
            }

            return PartialView(new LayoutVM { categories = ListCategory, isMenu = codeMenu.Product, listProduct = pageProduct, MaxPrice = maxPrirce });
        }

        [HttpPost]
        public IActionResult GetData(RequestSearch request)
        {
            var ListCategory = _categoryService.GetTree();
            var pageProduct = new PaginatedList<ProductVM>();

            var listProduct = _productService.GetAll();

            var maxPrirce = listProduct.Max(x => x.Sale);

            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.SearchName))
                {
                    listProduct = listProduct.Where(x => x.Name.Contains(request.SearchName, StringComparison.OrdinalIgnoreCase));
                }

                if (request.Categories?.Count() > 0)
                {
                    listProduct = listProduct.Where(x => request.Categories.Any(t => t.Equals(x.CategoryCode)));
                }

                if (request?.FromRange >= 0 && request.ToRange > 0)
                {
                    listProduct = listProduct.Where(x => x.Sale >= request.FromRange && x.Sale <= request.ToRange);
                }

                if (!string.IsNullOrEmpty(request?.SortPrice))
                {
                    var sort = (int)ConstantsPaging.AscPrice;
                    if (int.Parse(request.SortPrice) == sort)
                    {
                        listProduct = listProduct.OrderBy(o => o.Sale);
                    }
                    else
                    {
                        listProduct = listProduct.OrderByDescending(o => o.Sale);
                    }
                }
            }


            if (listProduct != null && listProduct.Count() > 0)
            {
                pageProduct = PaginatedList<ProductVM>.CreateAsync(listProduct.AsQueryable(), request?.PageNumber ?? 1, (int)ConstantsPaging.PageSize);
            }

            return Json(new LayoutVM { categories = ListCategory, isMenu = codeMenu.Product, listProduct = pageProduct, totalPages = pageProduct.TotalPages, hasNextPage = pageProduct.HasNextPage, hasPreviosPage = pageProduct.HasPreviousPage, pageIndex = pageProduct.PageIndex });
        }

        [HttpGet]
        [Route("/Product/Detail/{code}")]
        public IActionResult Detail(string code)
        {
            ViewBag.ListCategory = _categoryService.GetTree();
            ViewBag.ProductDetail = _productService.GetOne(code);
            return View(new LayoutVM { categories = ViewBag.ListCategory, isMenu = codeMenu.Product });
        }
    }
}

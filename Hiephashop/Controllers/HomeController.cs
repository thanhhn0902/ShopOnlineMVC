using Hiephashop.Application.DTOs.Category;
using Hiephashop.Application.DTOs.Layout;
using Hiephashop.Application.DTOs.Product;
using Hiephashop.Application.DTOs.Setting;
using Hiephashop.Application.Service.Categories;
using Hiephashop.Application.Service.products;
using Hiephashop.Application.Service.Setting;
using Hiephashop.Application.Service.Suppliers;
using Hiephashop.Data;
using Hiephashop.Data.Entities;
using Hiephashop.Models;
using Hiephashop.Ultility.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace Hiephashop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        private readonly IProductService _productService;
        private readonly ISettingService _settingService;
        public HomeController(ILogger<HomeController> logger, ICategoryService categoryService, ISupplierService supplierService, IProductService productService, ISettingService settingService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _supplierService = supplierService;
            _productService = productService;
            _settingService = settingService;
        }
        
        public IActionResult Index()
        {
            LoadLayout();
            ViewBag.ListCategory = _categoryService.GetTree();
            ViewBag.ListSupplier = _supplierService.GetAll();
            ViewBag.ListProduct = _productService.GetAll().Select(x => new ProductVM
            {
                Code = x.Code,
                Name = x.Name,
                Decription = x.Decription,
                Price = x.Price,
                Sale = x.Sale,
                Status = x.Status,
                CategoryCode = x.CategoryCode,
                SupplierCode = x.SupplierCode,
                Thumnail = x.Thumnail,
                PriceStr = x.PriceStr,
                SaleStr = x.SaleStr
            }).Take(16).ToList();
            return View(new LayoutVM { categories = ViewBag.ListCategory });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void LoadLayout()
        {
            var st = _settingService.GetOne();
            if (st != null)
            {
                var settingLayout = new SettingVM
                {
                    ShopNameLeft = st.ShopNameLeft ?? ConstantsLayout.ShopNameLeft,
                    ShopNameRight = st.ShopNameRight ?? ConstantsLayout.ShopNameRight,
                    Phone = st.Phone ?? ConstantsLayout.Phone,
                    Email = st.Email ?? ConstantsLayout.Email,
                    Address = st.Address ?? ConstantsLayout.Address,
                    GoogleAddress = st.GoogleAddress ?? ConstantsLayout.GoogleAddress,
                    Twitter = st.Twitter ?? ConstantsLayout.Twitter,
                    FaceBook = st.FaceBook ?? ConstantsLayout.FaceBook,
                    Instagram = st.Instagram ?? ConstantsLayout.Instagram,
                    Banner1 = st.InfoImage == null || st.InfoImage?.Count() < 1 ? ConstantsLayout.Banner1 : st.InfoImage[0]?.Url,
                    Banner2 = st.InfoImage == null || st.InfoImage?.Count() < 2 ? ConstantsLayout.Banner2 : st.InfoImage[1]?.Url,
                    Banner3 = st.InfoImage == null || st.InfoImage?.Count() < 3 ? ConstantsLayout.Banner3 : st.InfoImage[2]?.Url,
                    Banner4 = st.InfoImage == null || st.InfoImage?.Count() < 4 ? ConstantsLayout.Banner4 : st.InfoImage[3]?.Url,
                    Banner5 = st.InfoImage == null || st.InfoImage?.Count() < 5 ? ConstantsLayout.Banner5 : st.InfoImage[4]?.Url,
                    Title1 = st.Title1 ?? ConstantsLayout.Title1,
                    Title2 = st.Title2 ?? ConstantsLayout.Title2,
                    Title3 = st.Title3 ?? ConstantsLayout.Title3,
                    Content1 = st.Content1 ?? ConstantsLayout.Content1,
                    Content2 = st.Content2 ?? ConstantsLayout.Content2,
                    Content3 = st.Content3 ?? ConstantsLayout.Content3

                };

                HttpContext.Session.SetString(SessionName.SessionSetting, JsonConvert.SerializeObject(settingLayout));
            }

        }
    }
}
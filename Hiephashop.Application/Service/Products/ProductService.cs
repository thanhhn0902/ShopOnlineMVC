using Hiephashop.Data;
using Microsoft.EntityFrameworkCore;
using Hiephashop.Ultility.Constants;
using Hiephashop.Data.Entities;
using Hiephashop.Application.Service.Files;
using Hiephashop.Application.DTOs.Product;
using Microsoft.Extensions.Logging;
using Hiephashop.Application.Service.products;
using Microsoft.EntityFrameworkCore.Internal;
using Hiephashop.Ultility.Common;

namespace Hiephashop.Application.Service.products
{
    public class ProductService : IProductService
    {
        private readonly DbContextOptions<ShopDbContext> _options;
        private readonly IFileService _fileService;
        private readonly ILogger<ProductService> _logger;
        public ProductService(DbContextOptions<ShopDbContext> options, IFileService fileService, ILogger<ProductService> logger)
        {
            _options = options;
            _fileService = fileService;
            _logger = logger;
        }

        public StatusCRUD Create(ProductRequest request)
        {
            try
            {
                if (request == null) return StatusCRUD.Error;
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var saveFile = StatusCRUD.Success;

                    // check exist
                    var check = dbcontext.Products.Count(t => t.Code.Equals(request.Code));
                    if (check > 0)
                    {
                        return StatusCRUD.Duplicate;
                    }

                    // save metadata
                    var obj = new Product
                    {
                        Code = request.Code,
                        Name = request.Name,
                        Summary = request.Summary,
                        Decription = request.Decription,
                        Price = request.Price,
                        Sale = request.Sale,
                        CategoryCode = request.CategoryCode,
                        SupplierCode = request.SupplierCode,
                        Status = true,
                        Creator = "demo"
                    };
                    dbcontext.Add(obj);

                    //save detail
                    if (request.ListDetail.Count() > 0)
                    {
                        var listDetail = new List<ProductDetail>();
                        foreach (var item in request.ListDetail)
                        {
                            var detail = new ProductDetail
                            {
                                Title = item.Title,
                                Decription = item.Decription,
                                Order = item.Order,
                                Status = item.Status,
                                IsBold = item.IsBold,
                                Creator = "demmo",
                                ProductCode = obj.Code,
                                CategoryId = item.CategoryId,
                            };
                            listDetail.Add(detail);
                        }
                        dbcontext.AddRange(listDetail);
                    }

                    // check save file
                    if (dbcontext.SaveChanges() > 0 && request.Images?.Count() > 0)
                    {
                        saveFile = _fileService.UploadFile(request.Images, request.Code);
                        if (saveFile.Equals(StatusCRUD.Error)) return StatusCRUD.Error;
                    }
                    return StatusCRUD.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCRUD.Error;
            }
        }

        public StatusCRUD Delete(string code)
        {
            try
            {
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var obj = dbcontext.Products.SingleOrDefault(t => t.Code.Equals(code));
                    if (obj == null)
                    {
                        return StatusCRUD.NotFound;
                    }
                    obj.Status = false;

                    dbcontext.Update(obj);
                    dbcontext.SaveChanges();
                    return StatusCRUD.Success;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCRUD.Error;
            }
        }

        public StatusCRUD Edit(ProductUpdate request)
        {
            try
            {
                var check = StatusCRUD.Success;
                if (request == null) return StatusCRUD.Error;
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var obj = dbcontext.Products.SingleOrDefault(t => t.Code.Equals(request.Code));

                    if (obj == null)
                    {
                        return StatusCRUD.NotFound;
                    }

                    obj.Name = request.Name;
                    obj.Summary = request.Summary;
                    obj.Decription = request.Decription;
                    obj.Price = request.Price;
                    obj.Sale = request.Sale;
                    obj.CategoryCode = request.CategoryCode;
                    obj.SupplierCode = request.SupplierCode;
                    obj.Updater = "demoUpdate";
                    obj.UpdateDate = DateTime.Now;

                    dbcontext.Products.Update(obj);

                    //delete detail
                    var del = dbcontext.ProductDetails.Where(p => p.ProductCode.Equals(obj.Code)).ToList();
                    if (del?.Count() > 0)
                    {
                        dbcontext.ProductDetails.RemoveRange(del);
                    }

                    //create detail
                    if (request.ListDetailInsert?.Count() > 0)
                    {
                        var insert = request.ListDetailInsert.Select((x, index) => new ProductDetail
                        {
                            CategoryId = x.CategoryId,
                            Title = x.Title,
                            Decription = x.Decription,
                            Order = index,
                            Status = x.Status,
                            IsBold = x.IsBold,
                            ProductCode = obj.Code,
                            Creator = "demo"
                        });
                        dbcontext.ProductDetails.AddRange(insert);
                    }

                    if (dbcontext.SaveChanges() <= 0) return StatusCRUD.Error;

                    if (request.ListFileDel?.Count() > 0)
                    {
                        check = _fileService.DeleteFile(request.ListFileDel);
                    }

                    if (request.Images != null)
                    {
                        check = _fileService.UploadFile(request.Images, obj.Code);
                    }
                    return check;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCRUD.Error;
            }
        }

        public IEnumerable<ProductVM> GetAll()
        {
            try
            {
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var list = dbcontext.Products.Where(o => o.Status).ToList();
                    var showList = new List<ProductVM>();
                    foreach (var item in list)
                    {
                        var files = dbcontext.Files.OrderBy(x => x.Order).FirstOrDefault(f => f.ProductCode.Equals(item.Code));

                        var data = new ProductVM
                        {
                            Code = item.Code,
                            Name = item.Name,
                            Summary = item.Summary,
                            Decription = item.Decription,
                            Price = item.Price,
                            Sale = item.Sale,
                            PriceStr = CommonFunction.FormatCurrency(item.Price == null ? "0" : item.Price.ToString()),
                            SaleStr = CommonFunction.FormatCurrency(item.Sale == null ? "0" : item.Sale.ToString()),
                            CategoryCode = item.CategoryCode,
                            SupplierCode = item.SupplierCode,
                            Thumnail = files == null ? null : files.Url,
                        };
                        showList.Add(data);
                    }

                    return showList;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<ProductVM>();
            }
        }

        public ProductUpdate GetOne(string code, string? categoryCode)
        {
            try
            {
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var obj = dbcontext.Products.SingleOrDefault(o => o.Status && o.Code.Equals(code));
                    if (obj == null) return new ProductUpdate();
                    var files = dbcontext.Files.Where(f => f.ProductCode.Equals(obj.Code)).OrderBy(o => o.Order).ToList();

                    if (string.IsNullOrEmpty(categoryCode)) categoryCode = obj.CategoryCode;

                    var listCate = dbcontext.CategoryDetails.Where(t => t.CategoryCode.Equals(categoryCode)).OrderBy(o => o.Order);

                    var listProduct = dbcontext.ProductDetails.Where(t => t.ProductCode.Equals(obj.Code));

                    var details = from c in listCate
                                  join p in listProduct on c.Id equals p.CategoryId into gj
                                  from product in gj.DefaultIfEmpty()
                                  select new ProductDetailUpdate
                                  {
                                      Id = (product == null ? Guid.Empty : product.Id),
                                      CategoryId = c.Id,
                                      Title = c.Title,
                                      Description = (product == null ? null : product.Decription),
                                      Order = c.Order,
                                      IsBold = (product == null ? false : product.IsBold),
                                      Status = (product == null ? true : product.Status)
                                  };

                    var newObj = new ProductUpdate
                    {
                        Code = obj.Code,
                        Name = obj.Name,
                        Summary = obj.Summary,
                        Decription = obj.Decription,
                        Price = obj.Price ?? 0,
                        Sale = obj.Sale ?? 0,
                        CategoryCode = obj.CategoryCode,
                        SupplierCode = obj.SupplierCode,
                        ListDetail = details.ToList(),
                        PriceStr = CommonFunction.FormatCurrency(obj.Price == null ? "0" : obj.Price.ToString()),
                        SaleStr = CommonFunction.FormatCurrency(obj.Sale == null ? "0" : obj.Sale.ToString())
                    };

                    if (files != null)
                    {
                        newObj.InfoImage = files;
                    }
                    return newObj;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ProductUpdate();
            }
        }

        public ProductDetail FindProduct(Guid id)
        {
            try
            {
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var obj = dbcontext.ProductDetails.SingleOrDefault(x => x.Id.Equals(id));
                    return obj;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ProductDetail();
            }
        }
    }
}

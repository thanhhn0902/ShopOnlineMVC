using Hiephashop.Data;
using Microsoft.EntityFrameworkCore;
using Hiephashop.Application.DTOs.Supplier;
using System.Xml.Linq;
using Hiephashop.Ultility.Constants;
using Hiephashop.Data.Entities;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Http;
using Hiephashop.Application.Service.Files;
using System.Net;
using Hiephashop.Application.DTOs.Category;
using Microsoft.Extensions.Logging;

namespace Hiephashop.Application.Service.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly DbContextOptions<ShopDbContext> _options;
        private readonly IFileService _fileService;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(DbContextOptions<ShopDbContext> options, IFileService fileService, ILogger<CategoryService> logger)
        {
            _options = options;
            _fileService = fileService;
            _logger = logger;
        }

        public StatusCRUD Create(CategoryRequest request)
        {
            try
            {
                if (request == null) return StatusCRUD.Error;
                using (var dbcontext = new ShopDbContext(_options))
                {
                    Guid? saveFile = null;

                    // check exist
                    var check = dbcontext.Categories.Count(t => t.Code.Equals(request.Code));
                    if (check > 0)
                    {
                        return StatusCRUD.Duplicate;
                    }

                    // check save file
                    if (request.Images.Count() > 0)
                    {
                        saveFile = _fileService.UploadFile(request.Images.First());

                        if (saveFile == null) return StatusCRUD.Error;
                    }

                    // save metadata
                    var obj = new Category
                    {
                        Code = request.Code,
                        Name = request.Name,
                        ParentCode = request.ParentCode,
                        Thumnail = saveFile,
                        Status = true,
                        Creator = "demo"
                    };
                    dbcontext.Add(obj);

                    //save detail
                    if (request.ListDetail.Count() > 0)
                    {
                        var listDetail = new List<CategoryDetail>();
                        foreach (var item in request.ListDetail)
                        {
                            var detail = new CategoryDetail
                            {
                                Title = item.Title,
                                Order = item.Order,
                                Status = item.Status,
                                IsBold = item.IsBold,
                                CategoryCode = obj.Code,
                                Creator = "demmo"
                            };
                            listDetail.Add(detail);
                        }
                        dbcontext.AddRange(listDetail);
                    }

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

        public StatusCRUD Delete(string code)
        {
            try
            {
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var obj = dbcontext.Categories.SingleOrDefault(t => t.Code.Equals(code));
                    if (obj == null)
                    {
                        return StatusCRUD.NotFound;
                    }
                    obj.Status = false;

                    if (string.IsNullOrEmpty(obj.ParentCode))
                    {
                        var child = dbcontext.Categories.Where(t => !string.IsNullOrEmpty(t.ParentCode) && t.ParentCode.Equals(obj.Code));
                        child.ToList().ForEach(o =>
                        {
                            o.Status = false;
                        });
                        dbcontext.UpdateRange(child);
                    }

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

        public StatusCRUD Edit(CategoryUpdate request)
        {
            try
            {
                if (request == null) return StatusCRUD.Error;
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var obj = dbcontext.Categories.SingleOrDefault(t => t.Code.Equals(request.Code));
                    var thumbnail = Guid.Empty;

                    if (obj == null)
                    {
                        return StatusCRUD.NotFound;
                    }

                    if (request.ListFileDel.Count() > 0)
                    {
                        _fileService.DeleteFile(request.ListFileDel);
                    }

                    if (request.Images != null)
                    {
                        thumbnail = _fileService.UploadFile(request.Images) ?? Guid.Empty;
                    }

                    //obj.Code = request.Code;
                    obj.Name = request.Name;
                    obj.ParentCode = request.ParentCode;
                    obj.Thumnail = thumbnail.Equals(Guid.Empty) ? obj.Thumnail : thumbnail;
                    obj.Updater = "demoUpdate";
                    obj.UpdateDate = DateTime.Now;

                    // delete detail
                    if (request.ListDetailDel?.Count() > 0)
                    {
                        foreach (var item in request.ListDetailDel)
                        {
                            var del = FindCategory(item);
                            if (del != null)
                            {
                                dbcontext.Remove(del);
                            }
                        }
                    }

                    // create detail
                    if (request.ListDetailInsert?.Count() > 0)
                    {
                        var insert = request.ListDetailInsert.Select((x, index) => new CategoryDetail
                        {
                            Title = x.Title,
                            Order = dbcontext.CategoryDetails.Where(c => c.CategoryCode.Equals(obj.Code)).Max(m => m.Order) + (index + 1),
                            Status = x.Status,
                            IsBold = x.IsBold,
                            CategoryCode = obj.Code,
                            Creator = "demo"
                        });
                        dbcontext.AddRange(insert);
                    }

                    // update detail
                    if (request.ListDetailUpdate?.Count() > 0)
                    {
                        foreach (var item in request.ListDetailUpdate)
                        {
                            var update = FindCategory(item.Id);
                            if (update != null)
                            {
                                update.Title = item.Title;
                                //update.Order = item.Order;
                                //update.IsBold = item.IsBold;
                                dbcontext.Update(update);
                            }
                        }
                    }

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

        public IEnumerable<CategoryRequest> GetAll()
        {
            try
            {
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var list = dbcontext.Categories.Where(o => o.Status).ToList();
                    var showList = new List<CategoryRequest>();
                    foreach (var item in list)
                    {
                        var file = dbcontext.Files.FirstOrDefault(f => f.Id.Equals(item.Thumnail));
                        var parent = list.FirstOrDefault(c => c.Code.Equals(item.ParentCode) && c.Status);

                        var data = new CategoryRequest
                        {
                            Code = item.Code,
                            Name = item.Name,
                            ParentCode = item.ParentCode,
                            Thumnail = file == null ? null : file.Url,
                            ParentName = parent == null ? null : parent.Name
                        };
                        showList.Add(data);
                    }

                    return showList;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<CategoryRequest>();
            }
        }

        public CategoryUpdate GetOne(string code)
        {
            try
            {
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var obj = dbcontext.Categories.SingleOrDefault(o => o.Status && o.Code.Equals(code));
                    if (obj == null) return new CategoryUpdate();
                    var file = dbcontext.Files.FirstOrDefault(f => f.Id.Equals(obj.Thumnail));
                    var details = dbcontext.CategoryDetails.Where(t => t.CategoryCode.Equals(obj.Code)).OrderBy(o => o.Order).Select(x => new CategoryDetailUpdate
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Order = x.Order,
                        IsBold = x.IsBold,
                        Status = x.Status
                    }).ToList();

                    var newObj = new CategoryUpdate
                    {
                        Code = obj.Code,
                        Name = obj.Name,
                        ParentCode = obj.ParentCode,
                        ListDetail = details
                    };
                    if (file != null)
                    {
                        newObj.Thumnail = file.Id;
                        newObj.InfoLogo = file;
                    }
                    return newObj;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new CategoryUpdate();
            }
        }

        public CategoryDetail FindCategory(Guid id)
        {
            try
            {
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var obj = dbcontext.CategoryDetails.SingleOrDefault(x => x.Id.Equals(id));
                    if (obj == null) return new CategoryDetail();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new CategoryDetail();
            }
        }

        public IEnumerable<CategoryVM> GetTree()
        {
            try
            {
                using (var dbcontext = new ShopDbContext(_options))
                {

                    var list = dbcontext.Categories.Where(o => o.Status && string.IsNullOrEmpty(o.ParentCode)).ToList() ;
                    var showList = new List<CategoryVM>();
                    foreach (var item in list)
                    {
                        var file = dbcontext.Files.FirstOrDefault(f => f.Id.Equals(item.Thumnail));
                        var childrens = new List<CategoryVM>();
                        dbcontext.Categories.Where(c => item.Code.Equals(c.ParentCode) && c.Status).ToList().ForEach(x =>
                        {
                            var fileChild = dbcontext.Files.FirstOrDefault(f => f.Id.Equals(x.Thumnail)) ?? null;
                            childrens.Add(new CategoryVM
                            {
                                Code = x.Code,
                                Name = x.Name,
                                ParentCode = x.ParentCode,
                                Thumnail = fileChild == null ? null : fileChild.Url,
                            });

                        });

                        var data = new CategoryVM
                        {
                            Code = item.Code,
                            Name = item.Name,
                            ParentCode = item.ParentCode,
                            Thumnail = file == null ? null : file.Url,
                            ListChild = childrens
                        };
                        showList.Add(data);
                    }

                    return showList;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Enumerable.Empty<CategoryVM>();
            }
        }
    }
}

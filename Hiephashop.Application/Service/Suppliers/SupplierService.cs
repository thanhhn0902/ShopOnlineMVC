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
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Hiephashop.Application.Service.Suppliers
{
    public class SupplierService : ISupplierService
    {
        private readonly DbContextOptions<ShopDbContext> _options;
        private readonly IFileService _fileService;
        private readonly ILogger<SupplierService> _logger;
        public SupplierService(DbContextOptions<ShopDbContext> options, IFileService fileService, ILogger<SupplierService> logger)
        {
            _options = options;
            _fileService = fileService;
            _logger = logger;
        }

        public StatusCRUD Create(SupplierRequest request)
        {
            try
            {
                if (request == null) return StatusCRUD.Error;
                using (var dbcontext = new ShopDbContext(_options))
                {
                    Guid? saveFile = null;

                    // check exist
                    var check = dbcontext.Suppliers.Count(t => t.Code.Equals(request.Code));
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
                    var obj = new Supplier
                    {
                        Code = request.Code,
                        Name = request.Name,
                        Decription = request.Decription,
                        Logo = saveFile,
                        Creator = "demo"
                    };

                    dbcontext.Add(obj);
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
                    var obj = dbcontext.Suppliers.SingleOrDefault(t => t.Code.Equals(code));
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

        public StatusCRUD Edit(SupplierUpdate request)
        {
            try
            {
                if (request == null) return StatusCRUD.Error;
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var obj = dbcontext.Suppliers.SingleOrDefault(t => t.Code.Equals(request.Code));
                    var logo = Guid.Empty;

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
                        logo = _fileService.UploadFile(request.Images) ?? Guid.Empty;
                    }

                    //obj.Code = request.Code;
                    obj.Name = request.Name;
                    obj.Decription = request.Decription;
                    obj.Logo = logo.Equals(Guid.Empty) ? obj.Logo : logo;
                    obj.Updater = "demoUpdate";
                    obj.UpdateDate = DateTime.Now;

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

        public IEnumerable<SupplierRequest> GetAll()
        {
            try
            {
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var list = dbcontext.Suppliers.Where(o => o.Status).ToList();
                    var showList = new List<SupplierRequest>();

                    foreach (var item in list)
                    {
                        var file = dbcontext.Files.FirstOrDefault(f => f.Id.Equals(item.Logo));
                        var data = new SupplierRequest
                        {
                            Code = item.Code,
                            Name = item.Name,
                            Decription = item.Decription,
                            Logo = file == null ? null : file.Url
                        };
                        showList.Add(data);
                    }
                    return showList;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<SupplierRequest>();
            }
        }

        public SupplierUpdate GetOne(string code)
        {
            try
            {
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var obj = dbcontext.Suppliers.SingleOrDefault(o => o.Status && o.Code.Equals(code));
                    if (obj == null) return new SupplierUpdate();
                    var file = dbcontext.Files.FirstOrDefault(f => f.Id.Equals(obj.Logo));
                    var newObj = new SupplierUpdate
                    {
                        Code = obj.Code,
                        Name = obj.Name,
                        Decription = obj.Decription
                    };
                    if (file != null)
                    {
                        newObj.Logo = file.Id;
                        newObj.InfoLogo = file;
                    }
                    return newObj;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new SupplierUpdate();
            }
        }
    }
}

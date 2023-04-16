using Hiephashop.Application.DTOs.File;
using Hiephashop.Data;
using Hiephashop.Data.Entities;
using Hiephashop.Ultility.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiephashop.Application.Service.Files
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        DbContextOptions<ShopDbContext> _options;
        private readonly string path;
        public FileService(ILogger<FileService> logger, DbContextOptions<ShopDbContext> options)
        {
            _logger = logger;
            _options = options;
            path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadFile");

            //create folder if not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public StatusCRUD Create(ShopDbContext dbcontext, FileRequest request)
        {
            try
            {
                if (request == null) return StatusCRUD.Error;
                var check = dbcontext.Files.Count(t => t.Id.Equals(request.Id));
                if (check > 0)
                {
                    return StatusCRUD.Duplicate;
                }
                var obj = new Data.Entities.Files
                {
                    Id = request.Id,
                    Name = request.Name,
                    isImage = request.isImage,
                    Url = request.Url,
                    Order = request.Order,
                    ProductCode = request.ProductCode,
                    SettingCode = request.SettingCode,
                    Creator = "demo"
                };

                dbcontext.Add(obj);
                return StatusCRUD.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCRUD.Error;
            }
        }

        public StatusCRUD Delete(ShopDbContext dbcontext, Guid request)
        {
            try
            {
                if (request == Guid.Empty)
                {
                    _logger.LogWarning("File (0)");
                    return StatusCRUD.NotFound;
                };

                var obj = dbcontext.Files.SingleOrDefault(t => t.Id.Equals(request));
                if (obj != null)
                    dbcontext.Remove(obj);

                dbcontext.SaveChanges();
                return StatusCRUD.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCRUD.Error;
            }
        }

        public StatusCRUD DeleteFile(IEnumerable<Guid> files)
        {
            try
            {
                if (files == null || files.Count() == 0)
                {
                    _logger.LogWarning("File (0)");
                    return StatusCRUD.NotFound;
                }
                using (var dbcontext = new ShopDbContext(_options))
                {
                    foreach (var fileId in files)
                    {
                        var file = dbcontext.Files.SingleOrDefault(t => t.Id == fileId);

                        if (file == null) return StatusCRUD.NotFound;

                        string pathFile = path + '/' + file.Name;
                        if (File.Exists(pathFile))
                        {
                            File.Delete(pathFile);
                            Delete(dbcontext, fileId);
                        }
                    }
                }
                return StatusCRUD.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCRUD.Error;
            }
        }

        public StatusCRUD UploadFile(IEnumerable<IFormFile> files, string productCode)
        {
            try
            {
                if (files == null || files.Count() == 0)
                {
                    _logger.LogWarning("File (0)");
                    return StatusCRUD.NotFound;
                }
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var check = StatusCRUD.Success;
                    var listFileProduct = new List<Data.Entities.Files>();
                    if (productCode.Equals(SessionName.SettingCode))
                    {
                        listFileProduct = dbcontext.Files.Where(x => productCode.Equals(x.SettingCode)).ToList();
                    }
                    else
                    {
                        listFileProduct = dbcontext.Files.Where(x => productCode.Equals(x.ProductCode)).ToList();
                    }

                    foreach (var file in files.Select((value, index) => new { index, value }))
                    {
                        //get file extension
                        FileInfo fileInfo = new FileInfo(file.value.FileName);
                        string fileName = Guid.NewGuid() + file.value.FileName;
                        string fileNameWithPath = Path.Combine(path, fileName);
                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            file.value.CopyTo(stream);
                        }

                        if (productCode.Equals(SessionName.SettingCode))
                        {
                            check = Create(dbcontext, new FileRequest
                            {
                                Name = fileName,
                                isImage = true,
                                Url = "/UploadFile/" + fileName,
                                Order = (listFileProduct?.Count() == 0 ? 0 : listFileProduct.Max(x => x.Order)) + file.index,
                                SettingCode = productCode,
                            });
                        }
                        else
                        {
                            check = Create(dbcontext, new FileRequest
                            {
                                Name = fileName,
                                isImage = true,
                                Url = "/UploadFile/" + fileName,
                                Order = (listFileProduct?.Count() == 0 ? 0 : listFileProduct.Max(x => x.Order)) + file.index,
                                ProductCode = productCode,
                            });
                        }

                    }
                    if (check.Equals(StatusCRUD.Error)) return StatusCRUD.Error;
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

        public Guid? UploadFile(IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    _logger.LogWarning("File (0)");
                    return null;
                }
                var Id = Guid.NewGuid();
                using (var dbcontext = new ShopDbContext(_options))
                {
                    //get file extension
                    FileInfo fileInfo = new FileInfo(file.FileName);
                    string fileName = Guid.NewGuid() + file.FileName;
                    string fileNameWithPath = Path.Combine(path, fileName);
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    var check = Create(dbcontext, new FileRequest
                    {
                        Id = Id,
                        Name = fileName,
                        isImage = true,
                        Url = "/UploadFile/" + fileName,
                        Order = 0,
                    });

                    if (!check.Equals(StatusCRUD.Success)) return null;

                    dbcontext.SaveChanges();
                }
                return Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}

using Hiephashop.Data;
using Microsoft.EntityFrameworkCore;
using Hiephashop.Ultility.Constants;
using Hiephashop.Data.Entities;
using Hiephashop.Application.Service.Files;
using Hiephashop.Application.DTOs.Setting;
using Microsoft.Extensions.Logging;
using Hiephashop.Application.Service.products;
using Microsoft.EntityFrameworkCore.Internal;
using Hiephashop.Ultility.Common;

namespace Hiephashop.Application.Service.Setting
{
    public class SettingService : ISettingService
    {
        private readonly DbContextOptions<ShopDbContext> _options;
        private readonly IFileService _fileService;
        private readonly ILogger<SettingService> _logger;
        public SettingService(DbContextOptions<ShopDbContext> options, IFileService fileService, ILogger<SettingService> logger)
        {
            _options = options;
            _fileService = fileService;
            _logger = logger;
        }

        public StatusCRUD Edit(SettingUpdate request)
        {
            try
            {
                var check = StatusCRUD.Success;
                if (request == null) return StatusCRUD.Error;
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var obj = dbcontext.SettingLayouts.SingleOrDefault(t => t.Code.Equals(request.Code));

                    if (obj == null)
                    {
                        obj = new SettingLayout
                        {
                            Code = SessionName.SettingCode,
                            ShopNameLeft = request.ShopNameLeft,
                            ShopNameRight = request.ShopNameRight,
                            Phone = request.Phone,
                            Email = request.Email,
                            Address = request.Address,
                            GoogleAddress = request.GoogleAddress,
                            Twitter = request.Twitter,
                            FaceBook = request.FaceBook,
                            Instagram = request.Instagram,
                            Title1 = request.Title1,
                            Title2 = request.Title2,
                            Title3 = request.Title3,
                            Content1 = request.Content1,
                            Content2 = request.Content2,
                            Content3 = request.Content3
                        };
                        dbcontext.SettingLayouts.Add(obj);
                    }
                    else
                    {
                        obj.ShopNameLeft = request.ShopNameLeft;
                        obj.ShopNameRight = request.ShopNameRight;
                        obj.Phone = request.Phone;
                        obj.Email = request.Email;
                        obj.Address = request.Address;
                        obj.GoogleAddress = request.GoogleAddress;
                        obj.Twitter = request.Twitter;
                        obj.FaceBook = request.FaceBook;
                        obj.Instagram = request.Instagram;
                        obj.Title1 = request.Title1;
                        obj.Title2 = request.Title2;
                        obj.Title3 = request.Title3;
                        obj.Content1 = request.Content1;
                        obj.Content2 = request.Content2;
                        obj.Content3 = request.Content3;

                        dbcontext.SettingLayouts.Update(obj);
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

        public SettingUpdate GetOne()
        {
            try
            {
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var obj = dbcontext.SettingLayouts.FirstOrDefault();
                    if (obj == null) return new SettingUpdate();
                    var files = dbcontext.Files.Where(f => f.SettingCode.Equals(SessionName.SettingCode)).OrderBy(o => o.Order).ToList();

                    var newObj = new SettingUpdate
                    {
                        Code = obj.Code,
                        ShopNameLeft = obj.ShopNameLeft,
                        ShopNameRight = obj.ShopNameRight,
                        Phone = obj.Phone,
                        Email = obj.Email,
                        Address = obj.Address,
                        GoogleAddress = obj.GoogleAddress,
                        Twitter = obj.Twitter,
                        FaceBook = obj.FaceBook,
                        Instagram = obj.Instagram,
                        Title1 = obj.Title1,
                        Title2 = obj.Title2,
                        Title3 = obj.Title3,
                        Content1 = obj.Content1,
                        Content2 = obj.Content2,
                        Content3 = obj.Content3
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
                return new SettingUpdate();
            }
        }
    }
}

using Hiephashop.Data;
using Microsoft.EntityFrameworkCore;
using Hiephashop.Application.DTOs.User;
using System.Xml.Linq;
using Hiephashop.Ultility.Constants;
using Hiephashop.Data.Entities;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Http;
using Hiephashop.Application.Service.Files;
using System.Net;
using Hiephashop.Ultility.Common;
using Microsoft.Extensions.Logging;

namespace Hiephashop.Application.Service.Users
{
    public class UserService : IUserService
    {
        private readonly DbContextOptions<ShopDbContext> _options;
        private readonly IFileService _fileService;
        private readonly ILogger<UserService> _logger;
        public UserService(DbContextOptions<ShopDbContext> options, IFileService fileService, ILogger<UserService> logger)
        {
            _options = options;
            _fileService = fileService;
            _logger = logger;
        }

        public StatusCRUD Create(UserRequest request)
        {
            try
            {
                if (request == null) return StatusCRUD.Error;
                using (var dbcontext = new ShopDbContext(_options))
                {
                    Guid? saveFile = null;

                    // check exist
                    var check = dbcontext.Users.Count(t => t.UserName.Equals(request.UserName));
                    if (check > 0)
                    {
                        return StatusCRUD.Duplicate;
                    }

                    // check save file
                    if (request.Images != null)
                    {
                        saveFile = _fileService.UploadFile(request.Images);

                        if (saveFile == null) return StatusCRUD.Error;
                    }

                    // save metadata
                    var obj = new User
                    {
                        UserName = request.UserName,
                        Password = CommonFunction.MD5Hash(request.Password),
                        Name = request.Name,
                        Phone = request.Phone,
                        Email = request.Email,
                        Address = request.Address,
                        Avatar = saveFile,
                        Status = true,
                        Role = request.Role,
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

        public StatusCRUD Delete(string username)
        {
            try
            {
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var obj = dbcontext.Users.SingleOrDefault(t => t.UserName.Equals(username));
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

        public StatusCRUD Edit(UserUpdate request)
        {
            try
            {
                if (request == null) return StatusCRUD.Error;
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var obj = dbcontext.Users.SingleOrDefault(t => t.UserName.Equals(request.UserName));
                    var avatar = Guid.Empty;

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
                        avatar = _fileService.UploadFile(request.Images) ?? Guid.Empty;
                    }

                    //obj.Code = request.Code;
                    //obj.UserName = request.UserName;
                    obj.Name = request.Name;
                    if (!string.IsNullOrEmpty(request.Password))
                    {
                        obj.Password = CommonFunction.MD5Hash(request.Password);
                    }
                    obj.Phone = request.Phone;
                    obj.Email = request.Email;
                    obj.Address = request.Address;
                    obj.Avatar = avatar.Equals(Guid.Empty) ? obj.Avatar : avatar;
                    obj.Status = request.Status;
                    obj.Role = request.Role;
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

        public IEnumerable<UserRequest> GetAll()
        {
            try
            {
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var list = dbcontext.Users.Where(o => o.Status).ToList();
                    var showList = new List<UserRequest>();

                    foreach (var item in list)
                    {
                        var file = dbcontext.Files.FirstOrDefault(f => f.Id.Equals(item.Avatar));
                        var data = new UserRequest
                        {
                            UserName = item.UserName,
                            Name = item.Name,
                            Phone = item.Phone,
                            Email = item.Email,
                            Address = item.Address,
                            Status = item.Status,
                            Role = item.Role,
                            Avatar = file == null ? "" : file.Url
                        };
                        showList.Add(data);
                    }
                    return showList;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<UserRequest>();
            }
        }

        public UserUpdate GetOne(string username)
        {
            try
            {
                using (var dbcontext = new ShopDbContext(_options))
                {
                    var obj = dbcontext.Users.SingleOrDefault(o => o.Status && o.UserName.Equals(username));
                    if (obj == null) return new UserUpdate();
                    var file = dbcontext.Files.FirstOrDefault(f => f.Id.Equals(obj.Avatar));
                    var newObj = new UserUpdate
                    {
                        UserName = obj.UserName,
                        Name = obj.Name,
                        Phone = obj.Phone,
                        Email = obj.Email,
                        Address = obj.Address,
                        Status = obj.Status,
                        Role = obj.Role,
                    };
                    if (file != null)
                    {
                        newObj.Avatar = file.Id;
                        newObj.InfoLogo = file;
                    }
                    return newObj;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new UserUpdate();
            }
        }
    }
}

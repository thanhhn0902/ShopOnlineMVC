using Hiephashop.Application.DTOs.File;
using Hiephashop.Data;
using Hiephashop.Ultility.Constants;
using Microsoft.AspNetCore.Http;


namespace Hiephashop.Application.Service.Files
{
    public interface IFileService
    {
        StatusCRUD UploadFile(IEnumerable<IFormFile> files, string productCode);
        Guid? UploadFile(IFormFile file);
        StatusCRUD DeleteFile(IEnumerable<Guid> files);
        public StatusCRUD Create(ShopDbContext dbcontext, FileRequest request);
        public StatusCRUD Delete(ShopDbContext dbcontext, Guid request);
    }
}

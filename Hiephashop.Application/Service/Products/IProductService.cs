using Hiephashop.Application.DTOs.Product;
using Hiephashop.Ultility.Constants;

namespace Hiephashop.Application.Service.products
{
    public interface IProductService
    {
        public IEnumerable<ProductVM> GetAll();
        public ProductUpdate GetOne(string code, string? categoryCode = null);
        public StatusCRUD Create(ProductRequest request);
        public StatusCRUD Delete(string code);
        public StatusCRUD Edit(ProductUpdate request);
    }
}

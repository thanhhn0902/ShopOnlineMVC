using Hiephashop.Application.DTOs.Category;
using Hiephashop.Application.DTOs.Supplier;
using Hiephashop.Ultility.Constants;

namespace Hiephashop.Application.Service.Categories
{
    public interface ICategoryService
    {
        public IEnumerable<CategoryRequest> GetAll();
        public CategoryUpdate GetOne(string code);
        public StatusCRUD Create(CategoryRequest request);
        public StatusCRUD Delete(string code);
        public StatusCRUD Edit(CategoryUpdate request);
        public IEnumerable<CategoryVM> GetTree();
    }
}

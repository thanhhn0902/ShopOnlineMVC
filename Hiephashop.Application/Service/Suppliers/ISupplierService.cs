using Hiephashop.Application.DTOs.Supplier;
using Hiephashop.Ultility.Constants;

namespace Hiephashop.Application.Service.Suppliers
{
    public interface ISupplierService
    {
        public IEnumerable<SupplierRequest> GetAll();
        public SupplierUpdate GetOne(string code);
        public StatusCRUD Create(SupplierRequest request);
        public StatusCRUD Delete(string code);
        public StatusCRUD Edit(SupplierUpdate request);
    }
}

using Hiephashop.Application.DTOs.User;
using Hiephashop.Ultility.Constants;

namespace Hiephashop.Application.Service.Users
{
    public interface IUserService
    {
        public IEnumerable<UserRequest> GetAll();
        public UserUpdate GetOne(string code);
        public StatusCRUD Create(UserRequest request);
        public StatusCRUD Delete(string code);
        public StatusCRUD Edit(UserUpdate request);
    }
}

using Hiephashop.Application.DTOs.Setting;
using Hiephashop.Ultility.Constants;

namespace Hiephashop.Application.Service.Setting
{
    public interface ISettingService
    {
        public SettingUpdate GetOne();
        public StatusCRUD Edit(SettingUpdate request);
    }
}

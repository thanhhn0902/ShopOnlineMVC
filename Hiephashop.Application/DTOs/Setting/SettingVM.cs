using Hiephashop.Ultility.Constants;
using Microsoft.AspNetCore.Http;

namespace Hiephashop.Application.DTOs.Setting
{
    public class SettingVM
    {
        public string Code { get; set; } = SessionName.SettingCode;
        public string ShopNameLeft { get; set; }
        public string ShopNameRight { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string GoogleAddress { get; set; }
        public string Twitter { get; set; }
        public string FaceBook { get; set; }
        public string Instagram { get; set; }
        public string Banner1 { get; set; }
        public string Banner2 { get; set; }
        public string Banner3 { get; set; }
        public string Banner4 { get; set; }
        public string Banner5 { get; set; }
        public string Title1 { get; set; } = ConstantsLayout.Title1;
        public string Title2 { get; set; } = ConstantsLayout.Title2;
        public string Title3 { get; set; } = ConstantsLayout.Title3;
        public string Content1 { get; set; } = ConstantsLayout.Content1;
        public string Content2 { get; set; } = ConstantsLayout.Content2;
        public string Content3 { get; set; } = ConstantsLayout.Content3;

    }
}

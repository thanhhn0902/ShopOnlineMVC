using Hiephashop.Ultility.Constants;
using Microsoft.AspNetCore.Http;

namespace Hiephashop.Application.DTOs.Setting
{
    public class SettingRequest
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
        public string? Thumnail { get; set; }
        public bool Status { get; set; }
        public IEnumerable<IFormFile> Images { get; set; } = new List<IFormFile>();
        public string ArrayJson { get; set; }
    }
}

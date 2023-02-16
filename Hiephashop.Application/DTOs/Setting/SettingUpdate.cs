using Hiephashop.Data.Entities;
using Hiephashop.Ultility.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hiephashop.Application.DTOs.Setting
{
    public class SettingUpdate
    {
        public string Code { get; set; } = SessionName.SettingCode;
        public string ShopNameLeft { get; set; } = ConstantsLayout.ShopNameLeft;
        public string ShopNameRight { get; set; } = ConstantsLayout.ShopNameRight;
        public string Phone { get; set; } = ConstantsLayout.Phone;
        public string Email { get; set; } = ConstantsLayout.Email;
        public string Address { get; set; } = ConstantsLayout.Address;
        public string GoogleAddress { get; set; } = ConstantsLayout.GoogleAddress;
        public string Twitter { get; set; } = ConstantsLayout.Twitter;
        public string FaceBook { get; set; } = ConstantsLayout.FaceBook;
        public string Instagram { get; set; } = ConstantsLayout.Instagram;
        public string Title1 { get; set; } = ConstantsLayout.Title1;
        public string Title2 { get; set; } = ConstantsLayout.Title2;
        public string Title3 { get; set; } = ConstantsLayout.Title3;
        public string Content1 { get; set; } = ConstantsLayout.Content1;
        public string Content2 { get; set; } = ConstantsLayout.Content2;
        public string Content3 { get; set; } = ConstantsLayout.Content3;
        public List<Files> InfoImage { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
        public IEnumerable<Guid> ListFileDel { get; set; } = new List<Guid>();
        public string? ArrayJsonFileDel { get; set; }
    }
}

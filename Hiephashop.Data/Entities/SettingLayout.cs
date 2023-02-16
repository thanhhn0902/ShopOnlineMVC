using Hiephashop.Ultility.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiephashop.Data.Entities
{
    public class SettingLayout
    {
        [Key]
        public string Code { get; set; } = SessionName.SettingCode;
        public string? ShopNameLeft { get; set; }
        public string? ShopNameRight { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? GoogleAddress { get; set; }
        public string? Twitter { get; set; }
        public string? FaceBook { get; set; }
        public string? Instagram { get; set; }
        public string? Title1 { get; set; }
        public string? Title2 { get; set; }
        public string? Title3 { get; set; }
        public string? Content1 { get; set; }
        public string? Content2 { get; set; }
        public string? Content3 { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiephashop.Data.Entities
{
    public class Files : BaseEntity
    {
        public string Name { get; set; }
        public bool isImage { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public string ProductCode { get; set; }
        public string? SettingCode { get; set; }
        public Product Product { get; set; }
        //public User User { get; set; }
    }
}

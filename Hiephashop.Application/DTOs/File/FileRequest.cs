using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiephashop.Application.DTOs.File
{
    public class FileRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool isImage { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public string ProductCode { get; set; }
        public string SettingCode { get; set; }
    }
}

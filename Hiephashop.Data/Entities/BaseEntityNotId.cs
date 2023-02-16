using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiephashop.Data.Entities
{
    public abstract class BaseEntityNotId
    {
        public string Creator { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? Updater { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}

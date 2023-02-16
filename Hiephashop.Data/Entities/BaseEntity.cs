using System.ComponentModel.DataAnnotations;

namespace Hiephashop.Data.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Creator { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? Updater { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}

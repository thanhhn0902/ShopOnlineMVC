using Hiephashop.Ultility.Constants;
using System.Reflection.Metadata;

namespace Hiephashop.Data.Entities
{
    public class User : BaseEntityNotId
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Guid? Avatar { get; set; }
        public bool Status { get; set; } = true;
        public int Role { get; set; } = (int)ConstantsRole.User;
        //public Files File { get; set; }
    }
}

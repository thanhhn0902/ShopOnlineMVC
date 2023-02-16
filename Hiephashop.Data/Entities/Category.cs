namespace Hiephashop.Data.Entities
{
    public class Category : BaseEntityNotId
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? ParentCode { get; set; }
        public Guid? Thumnail { get; set; }
        public bool Status { get; set; } = true;
        public ICollection<Product> Products { get; set; }
        public ICollection<CategoryDetail> CategoryDetails { get; set; }
    }
}

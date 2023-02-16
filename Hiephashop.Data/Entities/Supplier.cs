namespace Hiephashop.Data.Entities
{
    public class Supplier : BaseEntityNotId
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Decription { get; set; }
        public Guid? Logo { get; set; }
        public bool Status { get; set; } = true;
        public ICollection<Product> Products { get; set; }
    }
}

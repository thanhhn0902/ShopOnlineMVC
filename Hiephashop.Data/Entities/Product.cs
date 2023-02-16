namespace Hiephashop.Data.Entities
{
    public class Product : BaseEntityNotId
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Summary { get; set; }
        public string? Decription { get; set; }
        public double? Price { get; set; }
        public double? Sale { get; set; }
        //public string? Thumbnail { get; set; }
        public bool Status { get; set; }
        public IEnumerable<ProductDetail> ProductDetails { get; set; }
        public string CategoryCode { get; set; }
        public Category Category { get; set; }
        public string SupplierCode { get; set; }
        public Supplier Supplier { get; set; }
        public IEnumerable<Files> Files { get; set; }
    }
}

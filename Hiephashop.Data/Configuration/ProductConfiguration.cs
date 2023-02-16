using Hiephashop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiephashop.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(t => t.Code);
            builder.HasMany(pd => pd.ProductDetails).WithOne(t => t.Product).HasForeignKey(f => f.ProductCode);
            builder.HasOne(s => s.Supplier).WithMany(t => t.Products).HasForeignKey(o => o.SupplierCode);
            builder.HasOne(s => s.Category).WithMany(t => t.Products).HasForeignKey(o => o.CategoryCode);
            builder.HasMany(t => t.Files).WithOne(f => f.Product).HasForeignKey(o => o.ProductCode).IsRequired(false);
        }
    }
}

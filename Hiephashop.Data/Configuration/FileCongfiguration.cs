using Hiephashop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hiephashop.Data.Configuration
{
    public class FileCongfiguration : IEntityTypeConfiguration<Files>
    {
        public void Configure(EntityTypeBuilder<Files> builder)
        {
            builder.ToTable("File");
            //builder.Property(x=>x.ProductCode).IsRequired(false);
        }
    }
}

using Altin.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Altin.DataAccess.Persistence.Configurations;

public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
{
    public void Configure(EntityTypeBuilder<CategoryProduct> builder)
    {
        builder.HasKey(bc => new { bc.ProductId, bc.CategoryId });
        
        builder
            .HasOne(bc => bc.Product)
            .WithMany(b => b.CategoryProducts)
            .HasForeignKey(bc => bc.ProductId);
        
        builder
            .HasOne(bc => bc.Category)
            .WithMany(c => c.CategoryProducts)
            .HasForeignKey(bc => bc.CategoryId);
    }
}
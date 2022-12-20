using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
            new Product
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), Title = "Product 1",
                Description = "Lorem ipsum dolor....", Price = 1250
            },
            new Product
            {
                Id = new Guid("c9d4c569-49b6-656c-bd98-2d54a6982678"), Title = "Product 2",
                Description = "Lorem ipsum dolor....", Price = 1250
            }
        );
    }
}
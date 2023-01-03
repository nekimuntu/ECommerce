using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {               
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p=>p.ImageUrl).IsRequired().HasMaxLength(100);
            builder.Property(p=>p.Name).IsRequired().HasMaxLength(100);
            //SQL doesnt have format decimal...but it works
            builder.Property(p=>p.Price).HasColumnType("decimal(18,2)").HasMaxLength(6);
            builder.Property(p=>p.Description).IsRequired(false).HasMaxLength(600);
            //The migration do it automaticly, but for training purpose lets define the Foreign Keys FK
            //THe brand can have 1 or N products 
            builder.HasOne(b=>b.ProductBrand).WithMany()
                .HasForeignKey(p=> p.ProductBrandId);
            //Type of product can have 1 or N products
            builder.HasOne(t=>t.ProductType).WithMany()
                .HasForeignKey(t=> t.ProductTypeId);
           // TODO: Just as an exercice chek how to use the method below
           // builder.HasMany("brandId").WithOne("ProductBrand.BrandId");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options){

        }
        public  DbSet<Product> Products { get; set; } //cree avec le shortcut prop

        public DbSet<ProductBrand> Brands { get; set; }

        public DbSet<ProductType> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            ///summary
            ///This following line is needed when you custom the Configuration for the Migration
            ///On tutorial the base.OnModelCreating is first...
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);            
        }
    }
}   
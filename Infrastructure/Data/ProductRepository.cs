using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                                    .Include(p => p.ProductBrand)
                                    .Include(p => p.ProductType)
                                    .FirstOrDefaultAsync(p=>p.Id==id);
            //Can also use SingleOrDefaultAsync() but this one will rise an exception if there is more than one entity found in Db

        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products
                                    .Include(p => p.ProductBrand)
                                    .Include(p => p.ProductType)
                                    .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductsBrandAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductsTypeAsync()
        {
            return await _context.Types.ToListAsync();
        }
    }
}
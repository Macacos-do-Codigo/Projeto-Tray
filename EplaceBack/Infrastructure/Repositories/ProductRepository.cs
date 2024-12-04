using Core.Models;
using Core.Repositories;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly OrdersDbContext _context;

        public ProductRepository(OrdersDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            List<Product> products = await _context
               .Products
               .ToListAsync();

            return products;
        }

        public async Task<Product> SaveProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }
}

using Core.Models;

namespace Core.Repositories
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetProducts();
        public Task<Product> SaveProduct(Product product);

    }
}

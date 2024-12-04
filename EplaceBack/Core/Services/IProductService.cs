using Core.DTOs;
using Core.Models;

namespace Core.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetProducts();

        public Task<Product> SaveProduct(ProductDTO productDTO);

    }
}

using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Core.Services;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public async Task<Product> SaveProduct(ProductDTO productDTO)
        {
            Product product = new Product(
                name: productDTO.Name,
                brand: productDTO.Brand,
                price: productDTO.Price,
                stockQuantity: productDTO.StockQuantity,
                mercadoLibre: productDTO.MercadoLibre,
                aliExpress: productDTO.AliExpress,
                shopee: productDTO.Shopee
            );

            product = await _productRepository.SaveProduct(product);
            return product;
        }
    }
}

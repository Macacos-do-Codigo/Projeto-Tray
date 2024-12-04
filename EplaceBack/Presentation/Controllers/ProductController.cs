using Core.DTOs;
using Core.Services;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAuthService _authService;

        public ProductController(IProductService productService, IAuthService authService)
        {
            _productService = productService;
            _authService = authService;
        }

        [HttpGet]
        public async Task<List<Product>> GetProductss()
        {
            string userId = _authService.GetAuthenticatedUserId(User);
            List<Product> products = await _productService.GetProducts();

            return products;
        }

        [HttpPost("SaveProduct")]
        public async Task<ActionResult<string>> SaveProduct(ProductDTO productDTO)
        {
            string userId = _authService.GetAuthenticatedUserId(User);
            Product product = await _productService.SaveProduct(productDTO);

            return CreatedAtAction(nameof(SaveProduct), new { id = product.Id }, product);
        }


    }
}

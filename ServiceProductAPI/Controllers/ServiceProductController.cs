using Microsoft.AspNetCore.Mvc;
using ServiceProductAPI.Services;
using ServiceProductAPI.Models;

namespace ServiceProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetCatalog()
        {
            var products = await _productService.GetProductCatalogAsync();

                if (products == null || !products.Any())
                {
                    return NotFound("No products found in the AdventureWorks catalog.");
                }
            return Ok(products);
        }
    }
}
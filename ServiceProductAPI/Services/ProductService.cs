using Microsoft.EntityFrameworkCore;
using ServiceProductAPI.Data;
using ServiceProductAPI.Models;

namespace ServiceProductAPI.Services
{
    public class ProductService:IProductService
    {
        private readonly ProductDbContext _context;

        public ProductService(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductCatalogAsync()
        {
            return await (from p in _context.Products
                          join bridge in _context.productModelProductDescriptionCultures
                               on p.ProductModelID equals bridge.ProductModelID
                          join desc in _context.ProductDescriptions
                               on bridge.ProductDescriptionID equals desc.ProductDescriptionID
                          where p.ListPrice > 0 && bridge.CultureID == "en"
                          select new ProductDTO
                          {
                              ProductID = p.ProductID,
                              Name = p.Name,
                              ListPrice = p.ListPrice,
                              Description = desc.Description
                          }).ToListAsync();
        }
    }
}

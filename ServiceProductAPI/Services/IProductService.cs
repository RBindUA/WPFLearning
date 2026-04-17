using ServiceProductAPI.Models;

namespace ServiceProductAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProductCatalogAsync();
    }
}

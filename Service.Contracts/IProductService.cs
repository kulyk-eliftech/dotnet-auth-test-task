using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync(bool trackChanges);
    Task<ProductDto> GetProductAsync(Guid id, bool trackChanges);
}
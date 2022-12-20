using Entities.Models;

namespace Contracts;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProducts(bool trackChanges);
    Task<Product> GetProduct(Guid id, bool trackChanges);
}
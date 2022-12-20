using Core.Entities;

namespace Core.Service;

public interface IProductService
{
    Task<Product> GetProduct(int id);
}
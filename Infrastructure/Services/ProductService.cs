using Core.Entities;
using Core.Service;

namespace Infrastructure.Services;

internal sealed class ProductService : IProductService
{
    public Task<Product> GetProduct(int id)
    {
        throw new NotImplementedException();
    }
}
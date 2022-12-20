using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    private readonly RepositoryContext _context;

    public ProductRepository(RepositoryContext context)
    {
        _context = context;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products!.FindAsync(id);
    }
}
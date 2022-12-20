using Contracts;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IProductRepository> _productRepository;
    private readonly RepositoryContext _repositoryContext;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _productRepository = new Lazy<IProductRepository>(() => new
            ProductRepository(repositoryContext));
    }

    public IProductRepository Product => _productRepository.Value;

    public void SaveAsync()
    {
        _repositoryContext.SaveChanges();
    }
}
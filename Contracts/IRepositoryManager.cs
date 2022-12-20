namespace Contracts;

public interface IRepositoryManager
{
    IProductRepository Product { get; }
    void SaveAsync();
}
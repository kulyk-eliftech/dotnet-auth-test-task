using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public class ProductService : IProductService
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repository;

    public ProductService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }


    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(bool trackChanges)
    {
        var products = await _repository.Product.GetAllProducts(trackChanges);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductAsync(Guid id, bool trackChanges)
    {
        var product = await _repository.Product.GetProduct(id, trackChanges);
        if (product is null) throw new ProductNotFoundException(id);

        return _mapper.Map<ProductDto>(product);
    }
}
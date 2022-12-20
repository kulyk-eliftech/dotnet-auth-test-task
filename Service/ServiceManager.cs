using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthService> _authService;
    private readonly Lazy<IProductService> _productService;
    private readonly Lazy<IUserService> _userService;

    public ServiceManager(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
        UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor)
    {
        _productService = new Lazy<IProductService>(() => new ProductService(repository, logger, mapper));
        _authService =
            new Lazy<IAuthService>(() => new AuthService(logger, mapper, userManager, roleManager, configuration));

        _userService =
            new Lazy<IUserService>(() =>
                new UserService(logger, mapper, userManager, httpContextAccessor, roleManager));
    }

    public IProductService ProductService => _productService.Value;
    public IAuthService AuthService => _authService.Value;
    public IUserService UserService => _userService.Value;
}
namespace Service.Contracts;

public interface IServiceManager
{
    IProductService ProductService { get; }
    IAuthService AuthService { get; }
    IUserService UserService { get; }
}
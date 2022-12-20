using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IAuthService
{
    Task<IdentityResult> RegisterUser(UserCreateDto userCreate);
    Task<bool> ValidateUser(UserLoginDto userLogin);
    Task<string> CreateToken();
}
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IUserService
{
    Task<string> CreateUser(UserCreateDto userCreate);
    Task<UserShowDto> GetUserProfile();
}
using AutoMapper;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;

    public UserService(ILoggerManager logger, IMapper mapper,
        UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _roleManager = roleManager;
    }

    public async Task<string> CreateUser(UserCreateDto userCreate)
    {
        var user = _mapper.Map<User>(userCreate);
        var result = await _userManager.CreateAsync(user, userCreate.Password);
        if (!result.Succeeded) throw new Exception("Cannot create user");

        foreach (var role in userCreate.Roles)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (roleExists) await _userManager.AddToRoleAsync(user, role);
        }

        return user.Id;
    }

    public async Task<UserShowDto> GetUserProfile()
    {
        var httpContext = _httpContextAccessor?.HttpContext ?? throw new UserNotFoundException();
        var user = await _userManager.GetUserAsync(httpContext.User) ?? throw new UserNotFoundException();
        return _mapper.Map<UserShowDto>(user);
    }
}
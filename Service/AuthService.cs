using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;

    private User _user;

    public AuthService(ILoggerManager logger, IMapper mapper,
        UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<IdentityResult> RegisterUser(UserCreateDto userCreate)
    {
        var user = _mapper.Map<User>(userCreate);
        var result = await _userManager.CreateAsync(user, userCreate.Password);
        if (!result.Succeeded) throw new Exception("User not created");

        foreach (var role in userCreate.Roles)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (roleExists) await _userManager.AddToRoleAsync(user, role);
        }

        return result;
    }

    public async Task<bool> ValidateUser(UserLoginDto userLogin)
    {
        _user = await _userManager.FindByNameAsync(userLogin.UserName);
        var result = _user != null && await _userManager.CheckPasswordAsync(_user,
            userLogin.Password);
        if (!result)
            _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong username or password.");

        return result;
    }

    public async Task<string> CreateToken()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await CreateClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];
        if (secretKey == null) throw new Exception("SecretKey is not installed");
        var key = Encoding.UTF8.GetBytes(secretKey);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> CreateClaims()
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, _user.UserName!),
            new(ClaimTypes.Email, _user.Email!),
            new(ClaimTypes.NameIdentifier, _user.Id!)
        };

        var roles = await _userManager.GetRolesAsync(_user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
        IEnumerable<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken
        (
            jwtSettings["Issuer"],
            jwtSettings["Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
}
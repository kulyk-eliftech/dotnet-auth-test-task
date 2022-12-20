using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace API.Controllers;

[Route("users")]
[Authorize]
public class UserController : BaseApiController
{
    private readonly IServiceManager _service;

    public UserController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet("current")]
    [Authorize]
    public async Task<IActionResult> Current()
    {
        var user = await _service.UserService.GetUserProfile();
        return Ok(user);
    }

    [HttpPost]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> CreateUser(UserCreateDto user)
    {
        var id = await _service.UserService.CreateUser(user);
        return Ok(id);
    }
}
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace API.Controllers;

[Route("auth")]
public class AuthController : BaseApiController
{
    private readonly IServiceManager _service;

    public AuthController(IServiceManager service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserCreateDto
        userForCreate)
    {
        var result = await _service.AuthService.RegisterUser(userForCreate);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors) ModelState.TryAddModelError(error.Code, error.Description);

            return BadRequest(ModelState);
        }

        return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] UserLoginDto user)
    {
        if (!await _service.AuthService.ValidateUser(user))
            return Unauthorized();

        return Ok(new { Token = await _service.AuthService.CreateToken() });
    }
}
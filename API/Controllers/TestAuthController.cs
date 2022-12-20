using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("tests")]
public class TestAuthController : BaseApiController
{
    [HttpGet("admin-only")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminOnly()
    {
        return Ok("Only for admin");
    }

    [HttpGet("user-only")]
    [Authorize(Roles = "User")]
    public IActionResult UserOnly()
    {
        return Ok("Only for user");
    }

    [HttpGet("authorized")]
    [Authorize]
    public IActionResult Authorized()
    {
        return Ok("Authorized only");
    }

    [HttpGet("for-all")]
    public IActionResult ForAll()
    {
        return Ok("For All");
    }
}
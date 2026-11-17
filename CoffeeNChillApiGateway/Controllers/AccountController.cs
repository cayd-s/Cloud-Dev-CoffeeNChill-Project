using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoffeeNChill.ApiGateway.Controllers;

[ApiController]
[Route("api/account")]
[Authorize]
public class AccountController : ControllerBase
{
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            email = User.Identity?.Name,
            role = User.FindFirst(ClaimTypes.Role)?.Value
        });
    }
}

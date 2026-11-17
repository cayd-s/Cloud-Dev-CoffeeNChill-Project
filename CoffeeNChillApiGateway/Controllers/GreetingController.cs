using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoffeeNChill.ApiGateway.Interfaces;
using CoffeeNChill.ApiGateway.DTOs;

namespace CoffeeNChill.ApiGateway.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GreetingController : ControllerBase
{
    private readonly IGreetingService _service;
    public GreetingController(IGreetingService service)
        => _service = service;

    [HttpPost]
    public IActionResult CreateGreeting([FromBody] GreetingRequestDto dto)
        => Ok(new { message = _service.CreateGreeting(dto.Name) });
}
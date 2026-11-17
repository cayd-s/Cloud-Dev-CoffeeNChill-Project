using Microsoft.AspNetCore.Mvc;

namespace CoffeeNChill.ApiGateway.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class WelcomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetWelcomeMessage() => Ok(new
        {
            Message = "Welcome to CoffeeNChill API Gateway",
            Status = "Success"
        });
    }
using CoffeeNChill.ApiGateway.Interfaces;

namespace CoffeeNChill.ApiGateway.Services;

public class GreetingService : IGreetingService
{
    public string CreateGreeting(string name)
    {
        return $"Hello {name}, welcome to CoffeeNChill!";
    }
}

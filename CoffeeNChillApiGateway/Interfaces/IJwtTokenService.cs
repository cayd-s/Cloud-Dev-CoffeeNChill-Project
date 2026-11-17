using CoffeeNChill.ApiGateway.DTOs;
using CoffeeNChill.ApiGateway.Models;

namespace CoffeeNChill.ApiGateway.Interfaces;

public interface IJwtTokenService
{
    LoginResponseDto GenerateToken(UserEntity user);
}
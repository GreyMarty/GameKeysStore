using System.Security.Claims;
using Infrastructure.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokensController : ApiControllerBase
{
    private readonly ITokenService _tokenService;

    public TokensController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpGet]
    public IActionResult GetToken([FromQuery] string login, [FromQuery] string password)
    {
        IActionResult response = Problem();

        _tokenService.CreateToken(login, password).Match(
            tokens => response = Ok(tokens),
            failure => response = BadRequest(failure),
            _ => response = NotFound()
        );

        return response;
    }

    [Authorize]
    [HttpGet]
    [Route("test")]
    public IActionResult Test()
    {
        return Ok(new
        {
            UserName = User.Identity?.Name,
            Roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
        });
    }
}
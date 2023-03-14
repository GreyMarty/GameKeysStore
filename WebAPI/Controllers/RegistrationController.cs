using Application.DTOs;
using Infrastructure.Security.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrationController : ApiControllerBase
{
    private readonly IIdentityService _identityService;

    public RegistrationController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost]
    public IActionResult RegisterUser(RegistrationUserDto model)
    {
        IActionResult response = Problem();

        _identityService.RegisterUser(model).Match(
            _ => response = Ok(),
            failure => response = BadRequest(failure)
        );

        return response;
    }
}
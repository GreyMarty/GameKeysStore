using Application.DTOs;
using Application.Services;
using AutoMapper;
using Infrastructure.Security;
using Infrastructure.Security.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ApiControllerBase
{
    private readonly IUserService _userService;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public UsersController(
        IUserService userService,
        IMapper mapper,
        IIdentityService identityService)
    {
        _userService = userService;
        _mapper = mapper;
        _identityService = identityService;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(_mapper.Map<IEnumerable<UserDto>>(_userService.GetAll()));
    }

    [HttpPost]
    [Route("{userId}/roles")]
    public IActionResult SetUserRoles(int userId, ulong roleFlags)
    {
        IActionResult response = Problem();

        _identityService.SetUserRoles(userId, (RoleFlags)roleFlags).Match(
            _ => response = Ok(),
            _ => response = NotFound()
        );

        return response;
    }
}
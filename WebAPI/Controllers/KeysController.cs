using Application.DTOs;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeysController : ApiControllerBase
{
    private readonly IKeyService _keyService;
    private readonly IMapper _mapper;

    public KeysController(IKeyService keyService, IMapper mapper)
    {
        _keyService = keyService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetKeys()
    {
        return Ok(_mapper.Map<IEnumerable<KeyDto>>(_keyService.GetAll()));
    }

    [HttpGet]
    [Route("/api/games/{gameId}/[controller]")]
    public IActionResult GetKeysForGame(int gameId)
    {
        return Ok(_mapper.Map<IEnumerable<KeyDto>>(_keyService.GetAllFor(gameId)));
    }

    [HttpPost]
    [Route("/api/games/{gameId}/[controller]")]
    public IActionResult CreateKey(int gameId, KeyDto model)
    {
        IActionResult response = Problem();

        _keyService.Create(gameId, model).Match(
            _ => response = Ok(),
            failure => response = BadRequest(failure),
            _ => response = NotFound()
        );

        return response;
    }

    [HttpPatch]
    [Route("{keyId}")]
    public IActionResult UpdateKey(int keyId, KeyDto model)
    {
        IActionResult response = Problem();

        _keyService.Update(keyId, model).Match(
            _ => response = Ok(),
            failure => response = BadRequest(failure),
            _ => response = NotFound()
        );

        return response;
    }

    [HttpDelete]
    [Route("{keyId}")]
    public IActionResult DeleteKey(int keyId)
    {
        IActionResult response = Problem();

        _keyService.Delete(keyId).Match(
            _ => response = Ok(),
            _ => response = NotFound()
        );

        return response;
    }
}
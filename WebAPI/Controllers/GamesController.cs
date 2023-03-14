using Application.DTOs;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly IMapper _mapper;

    public GamesController(IGameService gameService, IMapper mapper)
    {
        _gameService = gameService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetGames()
    {
        return Ok(_mapper.Map<IEnumerable<GameDto>>(_gameService.GetAll()));
    }


    [HttpPost]
    public IActionResult CreateGame(GameDto model)
    {
        IActionResult response = Problem();

        _gameService.Create(model)
            .Match(
                _ => response = Ok(),
                failed => response = BadRequest(failed)
            );

        return response;
    }

    [HttpPatch]
    [Route("{gameId}")]
    public IActionResult UpdateGame(int gameId, GameDto model)
    {
        IActionResult response = Problem();

        _gameService.Update(gameId, model)
            .Match(
                _ => response = Ok(),
                failed => response = BadRequest(failed),
                _ => response = NotFound()
            );

        return response;
    }

    [HttpDelete]
    [Route("{gameId}")]
    public IActionResult DeleteGame(int gameId)
    {
        IActionResult response = Problem();

        _gameService.Delete(gameId)
            .Match(
                _ => response = Ok(),
                _ => response = NotFound()
            );

        return response;
    }
}
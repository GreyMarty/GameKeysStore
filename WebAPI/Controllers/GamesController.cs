using Application.Models;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly IGamesService _gamesService;
    private readonly IMapper _mapper;

    public GamesController(IGamesService gamesService, IMapper mapper)
    {
        _gamesService = gamesService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetGames()
    {
        var games = _gamesService.GetAll();
        return Ok(games.Select(x => _mapper.Map<GameDto>(x)));
    }


    [HttpPost]
    public IActionResult CreateGame(GameDto model)
    {
        IActionResult response = Problem();

        _gamesService.Create(model)
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

        _gamesService.Update(gameId, model)
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

        _gamesService.Delete(gameId)
            .Match(
                _ => response = Ok(),
                _ => response = NotFound()
            );

        return response;
    }
}
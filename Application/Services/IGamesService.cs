using Application.DTOs;
using Application.Results;
using Domain.Entities;
using OneOf;
using OneOf.Types;

namespace Application.Services;

public interface IGamesService
{
    public IEnumerable<Game> GetAll();

    public OneOf<Game, ValidationFailed> Create(GameDto model);

    public OneOf<Game, ValidationFailed, NotFound> Update(int id, GameDto model);

    public OneOf<Success, NotFound> Delete(int gameId);
}
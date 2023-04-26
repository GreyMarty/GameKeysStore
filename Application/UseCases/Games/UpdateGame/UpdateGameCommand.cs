using Application.Models.ReadModels;
using Application.Models.WriteModels;
using MediatR;

namespace Application.UseCases.Games.UpdateGame;

public record UpdateGameCommand(int GameId, GameWriteModel Game) : IRequest<GameReadModel>;

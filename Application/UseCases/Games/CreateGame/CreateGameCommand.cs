using Application.Models.ReadModels;
using Application.Models.WriteModels;
using MediatR;

namespace Application.UseCases.Games.CreateGame;

public record CreateGameCommand(GameWriteModel Game) : IRequest<GameReadModel>;

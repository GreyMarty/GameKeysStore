using MediatR;

namespace Application.UseCases.Games.DeleteGame;

public record DeleteGameCommand(int Id) : IRequest;

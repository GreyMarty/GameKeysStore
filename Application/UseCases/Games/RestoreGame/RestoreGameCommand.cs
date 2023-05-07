using MediatR;

namespace Application.UseCases.Games.RestoreGame;

public record RestoreGameCommand(int Id) : IRequest;

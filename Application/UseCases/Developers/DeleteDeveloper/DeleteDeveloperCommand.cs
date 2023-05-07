using MediatR;

namespace Application.UseCases.Developers.DeleteDeveloper;

public record DeleteDeveloperCommand(int Id) : IRequest;

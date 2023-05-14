using MediatR;

namespace Application.UseCases.Keys.DeleteKey;

public record DeleteKeyCommand(int Id) : IRequest;

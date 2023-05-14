using MediatR;

namespace Application.UseCases.Platforms.DeletePlatform;

public record DeletePlatformCommand(int Id) : IRequest;

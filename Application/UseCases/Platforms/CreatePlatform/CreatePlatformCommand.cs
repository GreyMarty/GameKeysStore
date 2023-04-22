using Application.Models.ReadModels;
using Application.Models.WriteModels;
using MediatR;

namespace Application.UseCases.Platforms.CreatePlatform;

public record CreatePlatformCommand(PlatformWriteModel Platform) : IRequest<PlatformReadModel>;

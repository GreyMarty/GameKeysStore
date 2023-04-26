using Application.Data;
using Application.Models.ReadModels;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Platforms.GetPlatforms;

public record GetPlatformsQuery(Action<IQueryOptions<Platform>>? ConfigureOptions = null) : IRequest<IEnumerable<PlatformReadModel>>;

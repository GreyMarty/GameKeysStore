using Application.Data;
using Application.Models.ReadModels;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Platforms.GetPlatformsPaged;

public record GetPlatformsPagedQuery(int PageIndex, int PageSize, Action<IQueryOptions<Platform>>? ConfigureOptions = null) : IRequest<IPagedList<PlatformReadModel>>;

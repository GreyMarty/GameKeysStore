using Application.Data;
using Application.Models.ReadModels;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.GetGamesPaged;

public record GetGamesPagedQuery(int PageIndex, int PageSize, Action<IQueryOptions<Game>>? ConfigureOptions = null) : IRequest<IPagedList<GameReadModel>>;

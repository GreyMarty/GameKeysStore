using Application.Data;
using Application.Models.ReadModels;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Games.GetGamesSlice;

public record GetGamesSliceQuery(int Offset, int Count, Action<IQueryOptions<Game>>? ConfigureOptions = null) : IRequest<ISlice<GameReadModel>>;

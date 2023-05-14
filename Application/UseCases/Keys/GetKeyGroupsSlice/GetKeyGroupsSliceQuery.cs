using Application.Data;
using Application.Models.ReadModels;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Keys.GetKeyGroupsSlice;

public record GetKeyGroupsSliceQuery(int Offset, int Count, Action<IQueryOptions<Key>>? ConfigureOptions = null) : IRequest<ISlice<KeyGroupReadModel>>;

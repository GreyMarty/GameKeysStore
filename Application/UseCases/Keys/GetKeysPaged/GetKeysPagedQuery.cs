using Application.Data;
using Application.Models.ReadModels;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Keys.GetKeysPaged;

public record GetKeysPagedQuery(int PageIndex, int PageSize, Action<IQueryOptions<Key>>? ConfigureOptions = null) : IRequest<IPagedList<KeyReadModel>>;

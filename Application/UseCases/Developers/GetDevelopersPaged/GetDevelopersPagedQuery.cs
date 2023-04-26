using Application.Data;
using Application.Models.ReadModels;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Developers.GetDevelopersPaged;

public record GetDevelopersPagedQuery(int PageIndex, int PageSize, Action<IQueryOptions<Developer>>? ConfigureOptions = null) : IRequest<IEnumerable<DeveloperReadModel>>;

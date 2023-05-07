using Application.Data;
using Application.Models.ReadModels;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Users.GetUsersPaged;

public record GetUsersPagedQuery(int PageIndex, int PageSize, Action<IQueryOptions<User>>? ConfigureOptions = null) : IRequest<IPagedList<UserReadModel>>;

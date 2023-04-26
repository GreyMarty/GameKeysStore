using Application.Data;
using Application.Models.ReadModels;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Categories.GetCategories;

public record GetCategoriesQuery(Action<IQueryOptions<Category>>? ConfigureOptions = null) : IRequest<IEnumerable<CategoryReadModel>>;

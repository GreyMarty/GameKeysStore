using Application.Models.ReadModels;
using Application.Models.WriteModels;
using MediatR;

namespace Application.UseCases.Categories.CreateCategory;

public record CreateCategoryCommand(CategoryWriteModel Category) : IRequest<CategoryReadModel>;

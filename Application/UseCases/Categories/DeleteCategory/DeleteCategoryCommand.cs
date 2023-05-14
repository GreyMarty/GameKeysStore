using MediatR;

namespace Application.UseCases.Categories.DeleteCategory;

public record DeleteCategoryCommand(int Id) : IRequest;

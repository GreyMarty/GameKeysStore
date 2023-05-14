using MediatR;

namespace Application.UseCases.Categories.DeleteCategory;

internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IApplicationDbContext _db;

    public DeleteCategoryCommandHandler(IApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _db.Categories.FindAsync(request.Id);

        if (category is null) 
        {
            return;
        }

        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();
    }
}

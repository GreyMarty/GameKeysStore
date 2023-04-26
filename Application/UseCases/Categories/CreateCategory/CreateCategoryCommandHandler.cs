using Application.Exceptions;
using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Categories.CreateCategory;

internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryReadModel>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<CategoryReadModel> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (_db.Categories.Any(x => x.Name == request.Category.Name)) 
        {
            throw new EntityAlreadyExistsException("Category with the same name already exists", "Category.Name");
        }

        var category = _mapper.Map<Category>(request.Category);

        await _db.Categories.AddAsync(category);
        await _db.SaveChangesAsync();

        return _mapper.Map<CategoryReadModel>(category);
    }
}

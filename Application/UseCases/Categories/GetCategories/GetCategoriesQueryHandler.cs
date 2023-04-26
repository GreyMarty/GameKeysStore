using Application.Data;
using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Categories.GetCategories;

internal class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryReadModel>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryReadModel>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var options = new IncludableQueryOptions<Category>();
        options.OrderByAsc(x => x.Name);
        options.AsNoTracking();
        request.ConfigureOptions?.Invoke(options);

        var categories = await options.Apply(_db.Categories)
            .ToArrayAsync();

        return _mapper.Map<IEnumerable<CategoryReadModel>>(categories);
    }
}

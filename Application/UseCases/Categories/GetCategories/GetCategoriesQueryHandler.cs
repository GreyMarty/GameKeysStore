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
        request.ConfigureOptions?.Invoke(options);

        var dbCategories = _db.Categories
            .OrderBy(x => x.Name)
            .AsNoTracking();

        var categories = await options.Apply(dbCategories)
            .Select(x => new CategoryReadModel { Id = x.Id, Name = x.Name, GamesCount = x.Games.Count() })
            .ToArrayAsync();

        return categories;
    }
}

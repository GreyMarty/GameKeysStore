using Application.Data;
using AutoMapper;

namespace Application.Mapping;

internal class PagedListMapper: Profile
{
    public PagedListMapper()
    {
        CreateMap(typeof(IPagedList<>), typeof(IPagedList<>))
            .ConvertUsing(typeof(PagedListConverter<,>));
    }
}

internal class PagedListConverter<TSource, TDestination>
    : ITypeConverter<IPagedList<TSource>, IPagedList<TDestination>>
{
    public IPagedList<TDestination> Convert(
        IPagedList<TSource> source,
        IPagedList<TDestination> destination,
        ResolutionContext context)
    {
        return new PagedList<TDestination>(context.Mapper.Map<TDestination[]>(source.ToArray()), source.PageIndex, source.PageSize, source.TotalCount);
    }
}

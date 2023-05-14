using Application.Data;
using AutoMapper;

namespace Application.Mapping;

internal class SliceMapper : Profile
{
    public SliceMapper()
    {
        CreateMap(typeof(ISlice<>), typeof(ISlice<>))
            .ConvertUsing(typeof(SliceConverter<,>));
    }
}

internal class SliceConverter<TSource, TDestination>
    : ITypeConverter<ISlice<TSource>, ISlice<TDestination>>
{
    public ISlice<TDestination> Convert(
        ISlice<TSource> source,
        ISlice<TDestination> destination,
        ResolutionContext context)
    {
        return new Slice<TDestination>(context.Mapper.Map<TDestination[]>(source.ToArray()), source.Offset, source.TotalCount);
    }
}
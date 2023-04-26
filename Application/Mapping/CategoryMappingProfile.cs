using Application.Models.ReadModels;
using Application.Models.WriteModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

internal class CategoryMappingProfile : Profile
{
	public CategoryMappingProfile()
	{
		CreateMap<Category, CategoryReadModel>();
		CreateMap<CategoryReadModel, CategoryWriteModel>();
		CreateMap<CategoryWriteModel, Category>();
	}
}

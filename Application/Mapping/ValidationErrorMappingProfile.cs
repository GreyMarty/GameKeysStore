using Application.Validation;
using AutoMapper;
using FluentValidation.Results;

namespace Application.Mapping;

public class ValidationErrorMappingProfile : Profile
{
    public ValidationErrorMappingProfile()
    {
        CreateMap<ValidationFailure, ValidationError>();
    }
}
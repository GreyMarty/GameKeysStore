using Application.Models.ReadModels;
using Application.Models.WriteModels;
using MediatR;

namespace Application.UseCases.Developers.CreateDeveloper
{
    public record CreateDeveloperCommand(DeveloperWriteModel Developer) : IRequest<DeveloperReadModel>;
}
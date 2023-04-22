using Application.Models.ReadModels;
using Application.Models.WriteModels;
using MediatR;

namespace Application.UseCases.Keys.CreateKey;

public record CreateKeyCommand(KeyWriteModel Key) : IRequest<KeyReadModel>;

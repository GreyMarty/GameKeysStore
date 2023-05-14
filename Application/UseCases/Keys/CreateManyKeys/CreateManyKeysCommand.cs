using Application.Models.WriteModels;
using MediatR;

namespace Application.UseCases.Keys.CreateManyKeys;

public record class CreateManyKeysCommand(KeysWriteModel Keys) : IRequest;

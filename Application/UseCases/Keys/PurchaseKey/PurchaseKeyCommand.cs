using Application.Models.ReadModels;
using MediatR;

namespace Application.UseCases.Keys.PurchaseKey;

public record PurchaseKeyCommand(int GameId, int PlatformId) : IRequest<KeyReadModel>;

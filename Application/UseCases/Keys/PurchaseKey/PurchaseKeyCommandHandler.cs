using Application.Models.ReadModels;
using AutoMapper;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Keys.PurchaseKey;

internal class PurchaseKeyCommandHandler : IRequestHandler<PurchaseKeyCommand, KeyReadModel>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public PurchaseKeyCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<KeyReadModel> Handle(PurchaseKeyCommand request, CancellationToken cancellationToken)
    {
        var key = await _db.Keys
            .Where(x => x.GameId == request.GameId && x.PlatformId == request.PlatformId)
            .OrderBy(x => x.Price)
            .Include(x => x.Game)
            .FirstOrDefaultAsync();

        if (key is null) 
        {
            throw new EntityDoesNotExistException();
        }

        key.Purchased = true;
        await _db.SaveChangesAsync();

        return _mapper.Map<KeyReadModel>(key);
    }
}

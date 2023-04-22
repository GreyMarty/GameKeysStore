using Application.Exceptions;
using Application.Models.ReadModels;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Developers.CreateDeveloper;

internal class CreateDeveloperCommandHandler : IRequestHandler<CreateDeveloperCommand, DeveloperReadModel>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateDeveloperCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<DeveloperReadModel> Handle(CreateDeveloperCommand request, CancellationToken cancellationToken)
    {
        if (_db.Developers.Any(e => e.Name == request.Developer.Name)) 
        {
            throw new EntityAlreadyExistsException("Developer with the same name already exists", "Developer.Name");
        }

        var developer = _mapper.Map<Developer>(request.Developer);
        await _db.Developers.AddAsync(developer);
        await _db.SaveChangesAsync();

        return _mapper.Map<DeveloperReadModel>(developer);
    }
}

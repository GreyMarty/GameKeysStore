using Application.DTOs;
using Application.Results;
using Domain.Entities;
using OneOf;
using OneOf.Types;

namespace Application.Services;

public interface IKeyService
{
    public IEnumerable<Key> GetAll();

    public IEnumerable<Key> GetAllFor(int gameId);

    public OneOf<Key, ValidationFailed, NotFound> Create(int gameId, KeyDto model);

    public OneOf<Key, ValidationFailed, NotFound> Update(int keyId, KeyDto model);

    public OneOf<Success, NotFound> Delete(int keyId);
}
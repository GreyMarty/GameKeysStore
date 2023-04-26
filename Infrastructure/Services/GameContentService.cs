using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services;

public interface IGameContentService
{
    string GetImagesFolder(int gameId);
    Task<string?> MoveToImagesAsync(int gameId, string path);
}

[Service(ServiceLifetime.Scoped)]
public class GameContentService : IGameContentService
{
    IPhysicalStorage _storage;

    public GameContentService(IPhysicalStorage storage)
    {
        _storage = storage;
    }

    public Task<string?> MoveToImagesAsync(int gameId, string path) =>
        _storage.MoveFileAsync(path, Path.Combine(GetImagesFolder(gameId), Path.GetFileName(path)));

    public string GetImagesFolder(int gameId) =>
        Path.Combine("games", gameId.ToString(), "images");
}

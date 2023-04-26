using Application;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using PhysicalFile = Domain.Entities.PhysicalFile;

namespace Infrastructure.Services;

public interface IPhysicalStorage
{
    string ContentPath { get; }
    string TempPath { get; }

    Task<string?> CreateContentFileAsync(string path);
    Task<string?> CreateFileAsync(string relativeTo, string path);
    Task<string> CreateRandomTempFileAsync(string? extention = null);
    Task<string?> CreateTempFileAsync(string path);
    Task DeleteFileAsync(string path);
    bool FileExists(string path);
    string GetContentPath(string path);
    Task<string?> GetFilePathAsync(string path);
    string GetTempPath(string path);
    Task<string?> MoveFileAsync(string path, string newPath);
    Task<string?> MoveFileAsync(string relativeTo, string path, string newPath);
    Task<string?> MoveToTempAsync(string path);
    string NormalizePath(string path);
}

[Service(ServiceLifetime.Scoped)]
public class PhysicalStorage : IPhysicalStorage
{
    private const string ContentRootMacro = "${root}/";

    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _environment;
    private readonly IApplicationDbContext _db;

    public string ContentPath { get; }
    public string TempPath { get; }

    public PhysicalStorage(IConfiguration configuration, IHostEnvironment environment, IApplicationDbContext db)
    {
        _configuration = configuration;
        _environment = environment;

        ContentPath = (_configuration["ContentFolder"] ?? "content").Replace(ContentRootMacro, _environment.ContentRootPath);
        TempPath = Path.Combine(ContentPath, "temp");

        if (!Directory.Exists(ContentPath))
        {
            Directory.CreateDirectory(ContentPath);
        }

        if (!Directory.Exists(TempPath))
        {
            Directory.CreateDirectory(TempPath);
        }
        _db = db;
    }

    public bool FileExists(string path)
    {
        path = NormalizePath(path);
        return File.Exists(GetContentPath(path)) && _db.Files.Any(x => x.Path == path);
    }

    public string GetTempPath(string path) =>
        Path.Combine(TempPath, path);

    public string GetContentPath(string path) =>
        Path.Combine(ContentPath, path);

    public async Task<string?> GetFilePathAsync(string path) 
    {
        path = NormalizePath(path);
        var file = await _db.Files.FirstOrDefaultAsync(x => x.Path == path);

        if (file is null) 
        {
            return null;
        }

        return GetContentPath(file.Path);
    }

    public Task<string?> CreateTempFileAsync(string path) =>
        CreateFileAsync(ContentPath, GetTempPath(path));

    public Task<string> CreateRandomTempFileAsync(string? extention = null) =>
        CreateFileAsync(ContentPath, GetTempPath(Path.ChangeExtension(Path.GetRandomFileName(), extention)))!;

    public Task<string?> CreateContentFileAsync(string path) =>
        CreateFileAsync(ContentPath, GetContentPath(path));

    public Task<string?> MoveFileAsync(string path, string newPath) =>
        MoveFileAsync(ContentPath, GetContentPath(path), GetContentPath(newPath));

    public Task<string?> MoveToTempAsync(string path) =>
        MoveFileAsync(ContentPath, GetContentPath(path), GetTempPath(Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(path))));

    public async Task DeleteFileAsync(string path)
    {
        var file = await _db.Files.FirstOrDefaultAsync(x => x.Path == path);

        if (file is not null)
        {
            _db.Files.Remove(file);
            await _db.SaveChangesAsync();
        }

        path = GetContentPath(path);

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public async Task<string?> CreateFileAsync(string relativeTo, string path)
    {
        var folder = Path.GetDirectoryName(path);
        var fileName = Path.GetFileName(path);

        if (!string.IsNullOrWhiteSpace(folder) && !Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        if (string.IsNullOrWhiteSpace(fileName))
        {
            return null;
        }

        File.Create(path).Close();
        var file = new PhysicalFile() { Name = fileName, Path = NormalizePath(Path.GetRelativePath(relativeTo, path)) };

        await _db.Files.AddAsync(file);
        await _db.SaveChangesAsync();

        return file.Path;
    }

    public async Task<string?> MoveFileAsync(string relativeTo, string path, string newPath)
    {
        var relativePath = NormalizePath(Path.GetRelativePath(relativeTo, path));
        var file = await _db.Files.FirstOrDefaultAsync(x => x.Path == relativePath);

        if (file is null)
        {
            return null;
        }

        var folder = Path.GetDirectoryName(newPath);

        if (!string.IsNullOrWhiteSpace(folder) && !Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        File.Move(path, newPath);
        file.Path = NormalizePath(Path.GetRelativePath(relativeTo, newPath));

        _db.Files.Update(file);
        await _db.SaveChangesAsync();

        return file.Path;
    }

    public string NormalizePath(string path) =>
        Path.Combine(Path.GetDirectoryName(path) ?? string.Empty, Path.GetFileName(path) ?? string.Empty).Replace('\\', '/');
}

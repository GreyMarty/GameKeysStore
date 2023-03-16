using System.Security.Cryptography;
using System.Text;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Security.Helpers;

public interface IPasswordHelper
{
    string GenerateSalt();
    string ComputeHash(string password, string salt);
    bool VerifyPassword(string password, string passwordHash, string salt);
}

[Service(ServiceLifetime.Scoped)]
public class PasswordHelper : IPasswordHelper
{
    private const int KeySize = 64;
    private const int IterationsCount = 350000;
    private static readonly HashAlgorithmName HashAlgorithmName = HashAlgorithmName.SHA512;


    public string GenerateSalt()
    {
        var bytes = RandomNumberGenerator.GetBytes(KeySize);
        return Convert.ToBase64String(bytes);
    }

    public string ComputeHash(string password, string salt)
    {
        var hashBytes = ComputeHashBytes(password, salt);
        return Convert.ToBase64String(hashBytes);
    }

    public bool VerifyPassword(string password, string passwordHash, string salt)
    {
        return ComputeHash(password, salt) == passwordHash;
    }

    private byte[] ComputeHashBytes(string password, string salt)
    {
        return Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            Convert.FromBase64String(salt),
            IterationsCount,
            HashAlgorithmName,
            KeySize
        );
    }
}
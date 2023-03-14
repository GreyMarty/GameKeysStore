using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Security;

public static class PasswordHelper
{
    private const int KeySize = 64;
    private const int IterationsCount = 350000;
    private static readonly HashAlgorithmName HashAlgorithmName = HashAlgorithmName.SHA512;


    public static string GenerateSalt()
    {
        var bytes = RandomNumberGenerator.GetBytes(KeySize);
        return Convert.ToBase64String(bytes);
    }

    public static string ComputeHash(string password, string salt)
    {
        var hashBytes = ComputeHashBytes(password, salt);
        return Convert.ToBase64String(hashBytes);
    }

    public static bool VerifyPassword(string password, string passwordHash, string salt)
    {
        return ComputeHash(password, salt) == passwordHash;
    }

    private static byte[] ComputeHashBytes(string password, string salt)
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
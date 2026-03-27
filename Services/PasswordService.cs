using System.Security.Cryptography;
using System.Text;

namespace StationeryStore.Services;

public static class PasswordService
{
    public static string Hash(string password)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(hash);
    }
}

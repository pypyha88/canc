using System.Security.Cryptography;
using System.Text;

namespace StationeryStore.Services;

public static class PasswordService
{
    public static string Hash(string password)
    {
        using (var sha = SHA256.Create())
        {
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sb = new StringBuilder(hash.Length * 2);
            foreach (var b in hash) sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
    }
}

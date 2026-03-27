using System.Text.RegularExpressions;

namespace StationeryStore.Services;

public static class ValidationService
{
    public static bool IsEmailValid(string email) =>
        Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

    public static bool IsPhoneValid(string phone) =>
        Regex.IsMatch(phone, @"^\+?[0-9]{10,15}$");

    public static bool IsPasswordValid(string password) => password.Length >= 6;
}

using Microsoft.Data.SqlClient;
using StationeryStore.Data;
using StationeryStore.Models;

namespace StationeryStore.Services;

public class UserService
{
    public User? Login(string login, string password)
    {
        using var connection = Db.OpenConnection();
        using var cmd = new SqlCommand(@"
            SELECT UserId, FullName, Email, Phone, PasswordHash, RoleName
            FROM Users
            WHERE (Email = @login OR Phone = @login) AND PasswordHash = @passwordHash", connection);
        cmd.Parameters.AddWithValue("@login", login);
        cmd.Parameters.AddWithValue("@passwordHash", PasswordService.Hash(password));

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return null;

        return new User
        {
            UserId = reader.GetInt32(0),
            FullName = reader.GetString(1),
            Email = reader.GetString(2),
            Phone = reader.GetString(3),
            PasswordHash = reader.GetString(4),
            Role = Enum.Parse<UserRole>(reader.GetString(5), true)
        };
    }

    public void Register(string fullName, string email, string phone, string password)
    {
        if (!ValidationService.IsEmailValid(email)) throw new ArgumentException("Некорректный email.");
        if (!ValidationService.IsPhoneValid(phone)) throw new ArgumentException("Некорректный телефон.");
        if (!ValidationService.IsPasswordValid(password)) throw new ArgumentException("Пароль должен быть не менее 6 символов.");

        using var connection = Db.OpenConnection();
        using var cmd = new SqlCommand(@"
            INSERT INTO Users(FullName, Email, Phone, PasswordHash, RoleName)
            VALUES(@fullName, @email, @phone, @passwordHash, 'User')", connection);

        cmd.Parameters.AddWithValue("@fullName", fullName);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@phone", phone);
        cmd.Parameters.AddWithValue("@passwordHash", PasswordService.Hash(password));
        cmd.ExecuteNonQuery();
    }

    public void UpdateProfile(int userId, string fullName, string email, string phone)
    {
        if (!ValidationService.IsEmailValid(email)) throw new ArgumentException("Некорректный email.");
        if (!ValidationService.IsPhoneValid(phone)) throw new ArgumentException("Некорректный телефон.");

        using var connection = Db.OpenConnection();
        using var cmd = new SqlCommand(@"
            UPDATE Users
            SET FullName = @fullName, Email = @email, Phone = @phone
            WHERE UserId = @userId", connection);

        cmd.Parameters.AddWithValue("@userId", userId);
        cmd.Parameters.AddWithValue("@fullName", fullName);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@phone", phone);
        cmd.ExecuteNonQuery();
    }
}

using System.Data.SqlClient;

namespace StationeryStore.Data;

public static class Db
{
    public static SqlConnection OpenConnection()
    {
        var connectionString = StationeryStore.Program.Config.ConnectionStrings.DefaultConnection;
        var connection = new SqlConnection(connectionString);

        try
        {
            connection.Open();
            return connection;
        }
        catch (SqlException ex)
        {
            connection.Dispose();

            throw new InvalidOperationException(
                "Не удалось подключиться к SQL Server. Проверьте appsettings.json: " +
                "1) имя сервера (например, (localdb)\\MSSQLLocalDB или .\\SQLEXPRESS), " +
                "2) что служба SQL Server запущена, " +
                "3) что база StationeryStoreDb создана скриптом. " +
                $"Тех.детали: {ex.Message}");
        }
    }
}

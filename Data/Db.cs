using System.Data.SqlClient;

namespace StationeryStore.Data;

public static class Db
{
    public static SqlConnection OpenConnection()
    {
        var connection = new SqlConnection(StationeryStore.Program.Config.ConnectionStrings.DefaultConnection);
        connection.Open();
        return connection;
    }
}

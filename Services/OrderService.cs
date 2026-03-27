using System.Data.SqlClient;
using StationeryStore.Data;

namespace StationeryStore.Services;

public class OrderService
{
    public void CreateOrder(int userId, IEnumerable<(int ProductId, int Quantity, decimal UnitPrice)> items)
    {
        using var connection = Db.OpenConnection();
        using var transaction = connection.BeginTransaction();

        try
        {
            var total = items.Sum(i => i.Quantity * i.UnitPrice);
            using var orderCmd = new SqlCommand(@"
                INSERT INTO Orders(UserId, OrderDate, TotalAmount)
                OUTPUT INSERTED.OrderId
                VALUES(@userId, @orderDate, @total)", connection, transaction);
            orderCmd.Parameters.AddWithValue("@userId", userId);
            orderCmd.Parameters.AddWithValue("@orderDate", DateTime.UtcNow);
            orderCmd.Parameters.AddWithValue("@total", total);
            var orderId = (int)orderCmd.ExecuteScalar()!;

            foreach (var item in items)
            {
                using var itemCmd = new SqlCommand(@"
                    INSERT INTO OrderItems(OrderId, ProductId, Quantity, UnitPrice)
                    VALUES(@orderId, @productId, @quantity, @price)", connection, transaction);
                itemCmd.Parameters.AddWithValue("@orderId", orderId);
                itemCmd.Parameters.AddWithValue("@productId", item.ProductId);
                itemCmd.Parameters.AddWithValue("@quantity", item.Quantity);
                itemCmd.Parameters.AddWithValue("@price", item.UnitPrice);
                itemCmd.ExecuteNonQuery();
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}

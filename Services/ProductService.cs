using System.Data;
using Microsoft.Data.SqlClient;
using StationeryStore.Data;
using StationeryStore.Models;

namespace StationeryStore.Services;

public class ProductService
{
    public DataTable GetCatalog(string? search = null, string? sortColumn = null)
    {
        using var connection = Db.OpenConnection();
        var sql = @"SELECT ProductId, ProductName, Price, OldPrice, DiscountPercent, ImagePath, StockQty FROM Products";
        if (!string.IsNullOrWhiteSpace(search))
        {
            sql += " WHERE ProductName LIKE @search";
        }

        if (!string.IsNullOrWhiteSpace(sortColumn))
        {
            sql += sortColumn switch
            {
                "price_asc" => " ORDER BY Price ASC",
                "price_desc" => " ORDER BY Price DESC",
                _ => " ORDER BY ProductName"
            };
        }

        using var cmd = new SqlCommand(sql, connection);
        if (!string.IsNullOrWhiteSpace(search)) cmd.Parameters.AddWithValue("@search", $"%{search}%");
        using var adapter = new SqlDataAdapter(cmd);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public void Save(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.ProductName)) throw new ArgumentException("Введите название товара.");
        if (product.Price <= 0) throw new ArgumentException("Цена должна быть больше 0.");

        using var connection = Db.OpenConnection();
        SqlCommand cmd;

        if (product.ProductId == 0)
        {
            cmd = new SqlCommand(@"
                INSERT INTO Products(ProductName, Price, OldPrice, DiscountPercent, ImagePath, StockQty)
                VALUES(@name, @price, @oldPrice, @discount, @imagePath, @stockQty)", connection);
        }
        else
        {
            cmd = new SqlCommand(@"
                UPDATE Products
                SET ProductName=@name, Price=@price, OldPrice=@oldPrice, DiscountPercent=@discount, ImagePath=@imagePath, StockQty=@stockQty
                WHERE ProductId=@id", connection);
            cmd.Parameters.AddWithValue("@id", product.ProductId);
        }

        cmd.Parameters.AddWithValue("@name", product.ProductName);
        cmd.Parameters.AddWithValue("@price", product.Price);
        cmd.Parameters.AddWithValue("@oldPrice", (object?)product.OldPrice ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@discount", (object?)product.DiscountPercent ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@imagePath", (object?)product.ImagePath ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@stockQty", product.StockQty);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int productId)
    {
        using var connection = Db.OpenConnection();
        using var cmd = new SqlCommand("DELETE FROM Products WHERE ProductId=@id", connection);
        cmd.Parameters.AddWithValue("@id", productId);
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            throw new InvalidOperationException("Невозможно удалить товар, так как он присутствует в одном или нескольких заказах.");
        }
    }
}

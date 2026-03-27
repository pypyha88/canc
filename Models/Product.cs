namespace StationeryStore.Models;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public int? DiscountPercent { get; set; }
    public string? ImagePath { get; set; }
    public int StockQty { get; set; }

    public decimal FinalPrice => OldPrice.HasValue && DiscountPercent.HasValue
        ? Math.Round(OldPrice.Value * (1m - DiscountPercent.Value / 100m), 2)
        : Price;
}

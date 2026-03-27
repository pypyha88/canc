using StationeryStore.Models;
using StationeryStore.Services;

namespace StationeryStore.Forms;

public partial class ProductEditForm : Form
{
    private readonly int _productId;
    private string? _relativeImagePath;
    private readonly ProductService _productService = new();

    public ProductEditForm(int productId = 0)
    {
        _productId = productId;
        InitializeComponent();
        Text = productId == 0 ? "Добавить товар" : "Редактировать товар";
    }

    private void BtnLoadImage_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp",
            Title = "Выберите изображение товара"
        };

        if (dialog.ShowDialog() != DialogResult.OK) return;

        var targetDir = Path.Combine(AppContext.BaseDirectory, "ProductImages");
        Directory.CreateDirectory(targetDir);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dialog.FileName)}";
        var targetPath = Path.Combine(targetDir, fileName);
        File.Copy(dialog.FileName, targetPath, true);

        _relativeImagePath = Path.Combine("ProductImages", fileName);
        picturePreview.Image = Image.FromFile(targetPath);
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        try
        {
            var product = new Product
            {
                ProductId = _productId,
                ProductName = txtName.Text.Trim(),
                Price = numPrice.Value,
                OldPrice = numOldPrice.Value == 0 ? null : numOldPrice.Value,
                DiscountPercent = numDiscount.Value == 0 ? null : (int)numDiscount.Value,
                StockQty = (int)numStock.Value,
                ImagePath = _relativeImagePath
            };

            _productService.Save(product);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}

using StationeryStore.Models;
using StationeryStore.Services;

namespace StationeryStore.Forms;

public class ProductEditForm : Form
{
    private readonly int _productId;
    private string? _relativeImagePath;

    private readonly TextBox _txtName = new() { PlaceholderText = "Название" };
    private readonly NumericUpDown _numPrice = new() { Maximum = 1_000_000, DecimalPlaces = 2 };
    private readonly NumericUpDown _numOldPrice = new() { Maximum = 1_000_000, DecimalPlaces = 2 };
    private readonly NumericUpDown _numDiscount = new() { Maximum = 100 };
    private readonly NumericUpDown _numStock = new() { Maximum = 10_000 };
    private readonly PictureBox _picture = new() { Height = 120, Dock = DockStyle.Top, SizeMode = PictureBoxSizeMode.Zoom };
    private readonly Button _btnLoadImage = new() { Text = "Загрузить изображение" };
    private readonly Button _btnSave = new() { Text = "Сохранить" };

    private readonly ProductService _productService = new();

    public ProductEditForm(int productId = 0)
    {
        _productId = productId;

        Text = productId == 0 ? "Добавить товар" : "Редактировать товар";
        Width = 400;
        Height = 480;
        StartPosition = FormStartPosition.CenterParent;

        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), RowCount = 12, ColumnCount = 1 };
        layout.Controls.Add(_txtName);
        layout.Controls.Add(new Label { Text = "Цена" });
        layout.Controls.Add(_numPrice);
        layout.Controls.Add(new Label { Text = "Старая цена" });
        layout.Controls.Add(_numOldPrice);
        layout.Controls.Add(new Label { Text = "Скидка, %" });
        layout.Controls.Add(_numDiscount);
        layout.Controls.Add(new Label { Text = "Остаток" });
        layout.Controls.Add(_numStock);
        layout.Controls.Add(_picture);
        layout.Controls.Add(_btnLoadImage);
        layout.Controls.Add(_btnSave);

        Controls.Add(layout);

        _btnLoadImage.Click += (_, _) => UploadImage();
        _btnSave.Click += (_, _) => SaveProduct();
    }

    private void UploadImage()
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
        _picture.Image = Image.FromFile(targetPath);
    }

    private void SaveProduct()
    {
        try
        {
            var product = new Product
            {
                ProductId = _productId,
                ProductName = _txtName.Text.Trim(),
                Price = _numPrice.Value,
                OldPrice = _numOldPrice.Value == 0 ? null : _numOldPrice.Value,
                DiscountPercent = _numDiscount.Value == 0 ? null : (int)_numDiscount.Value,
                StockQty = (int)_numStock.Value,
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

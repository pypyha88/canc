using System.Data;
using StationeryStore.Models;
using StationeryStore.Services;

namespace StationeryStore.Forms;

public class MainForm : Form
{
    private readonly User _currentUser;
    private readonly ProductService _productService = new();
    private readonly UserService _userService = new();

    private readonly DataGridView _catalogGrid = new() { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
    private readonly TextBox _txtSearch = new() { PlaceholderText = "Поиск" };
    private readonly ComboBox _cbSort = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly Button _btnRefresh = new() { Text = "Обновить" };
    private readonly Button _btnAddProduct = new() { Text = "Добавить товар" };
    private readonly Button _btnEditProduct = new() { Text = "Редактировать" };
    private readonly Button _btnDeleteProduct = new() { Text = "Удалить" };

    private readonly TextBox _txtName = new();
    private readonly TextBox _txtEmail = new();
    private readonly TextBox _txtPhone = new();
    private readonly Button _btnSaveProfile = new() { Text = "Сохранить профиль", Name = "btnSaveChanges" };

    public MainForm(User currentUser)
    {
        _currentUser = currentUser;
        Text = $"Магазин канцелярии — {_currentUser.Role}";
        WindowState = FormWindowState.Maximized;

        var tabs = new TabControl { Dock = DockStyle.Fill };
        tabs.TabPages.Add(CreateCatalogPage());
        tabs.TabPages.Add(CreateProfilePage());
        Controls.Add(tabs);

        LoadCatalog();
        ApplyRolePermissions();
    }

    private TabPage CreateCatalogPage()
    {
        var page = new TabPage("Каталог");
        _cbSort.Items.AddRange(new object[] { "По названию", "Сначала дешевые", "Сначала дорогие" });
        _cbSort.SelectedIndex = 0;

        var topPanel = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40 };
        topPanel.Controls.AddRange(new Control[] { _txtSearch, _cbSort, _btnRefresh, _btnAddProduct, _btnEditProduct, _btnDeleteProduct });

        page.Controls.Add(_catalogGrid);
        page.Controls.Add(topPanel);

        _btnRefresh.Click += (_, _) => LoadCatalog();
        _txtSearch.TextChanged += (_, _) => LoadCatalog();
        _cbSort.SelectedIndexChanged += (_, _) => LoadCatalog();
        _btnAddProduct.Click += (_, _) => OpenProductEditor();
        _btnEditProduct.Click += (_, _) => OpenProductEditor(GetSelectedProductId());
        _btnDeleteProduct.Click += (_, _) => DeleteSelectedProduct();
        _catalogGrid.CellFormatting += CatalogGrid_CellFormatting;

        return page;
    }

    private TabPage CreateProfilePage()
    {
        var page = new TabPage("Личный кабинет");
        var layout = new TableLayoutPanel { Dock = DockStyle.Top, Height = 160, Padding = new Padding(20), RowCount = 8, ColumnCount = 1 };

        _txtName.Text = _currentUser.FullName;
        _txtEmail.Text = _currentUser.Email;
        _txtPhone.Text = _currentUser.Phone;

        layout.Controls.Add(new Label { Text = "Имя" });
        layout.Controls.Add(_txtName);
        layout.Controls.Add(new Label { Text = "Email" });
        layout.Controls.Add(_txtEmail);
        layout.Controls.Add(new Label { Text = "Телефон" });
        layout.Controls.Add(_txtPhone);
        layout.Controls.Add(_btnSaveProfile);

        _btnSaveProfile.Click += (_, _) =>
        {
            try
            {
                _userService.UpdateProfile(_currentUser.UserId, _txtName.Text.Trim(), _txtEmail.Text.Trim(), _txtPhone.Text.Trim());
                MessageBox.Show("Профиль сохранён.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        };

        page.Controls.Add(layout);
        return page;
    }

    private void LoadCatalog()
    {
        var sort = _cbSort.SelectedIndex switch
        {
            1 => "price_asc",
            2 => "price_desc",
            _ => "name"
        };

        var table = _productService.GetCatalog(_txtSearch.Text.Trim(), sort);
        _catalogGrid.DataSource = table;
    }

    private void CatalogGrid_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (_catalogGrid.Rows[e.RowIndex].DataBoundItem is not DataRowView rowView) return;
        var row = rowView.Row;

        if (row.Field<decimal>("Price") > 1000)
        {
            _catalogGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
            _catalogGrid.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(_catalogGrid.Font, FontStyle.Bold);
        }

        if (row["OldPrice"] != DBNull.Value && row["DiscountPercent"] != DBNull.Value)
        {
            var oldPrice = Convert.ToDecimal(row["OldPrice"]);
            var discount = Convert.ToInt32(row["DiscountPercent"]);
            var finalPrice = Math.Round(oldPrice * (1m - discount / 100m), 2);
            _catalogGrid.Rows[e.RowIndex].Cells[2].Value = finalPrice;
            _catalogGrid.Rows[e.RowIndex].Cells[3].Style.Font = new Font(_catalogGrid.Font, FontStyle.Strikeout);
        }
    }

    private int GetSelectedProductId()
    {
        if (_catalogGrid.CurrentRow?.Cells[0].Value is null)
            throw new InvalidOperationException("Выберите товар.");

        return Convert.ToInt32(_catalogGrid.CurrentRow.Cells[0].Value);
    }

    private void OpenProductEditor(int productId = 0)
    {
        try
        {
            using var form = new ProductEditForm(productId);
            if (form.ShowDialog() == DialogResult.OK)
                LoadCatalog();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void DeleteSelectedProduct()
    {
        try
        {
            _productService.Delete(GetSelectedProductId());
            LoadCatalog();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void ApplyRolePermissions()
    {
        var canEditProducts = _currentUser.Role is UserRole.Manager or UserRole.Admin;
        var canDelete = _currentUser.Role is UserRole.Admin;

        _btnAddProduct.Visible = canEditProducts;
        _btnEditProduct.Visible = canEditProducts;
        _btnDeleteProduct.Visible = canDelete;
    }
}

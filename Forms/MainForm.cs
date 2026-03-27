using System.Data;
using StationeryStore.Models;
using StationeryStore.Services;

namespace StationeryStore.Forms;

public partial class MainForm : Form
{
    private readonly User _currentUser;
    private readonly ProductService _productService = new();
    private readonly UserService _userService = new();

    public MainForm(User currentUser)
    {
        _currentUser = currentUser;
        InitializeComponent();
        Text = $"Магазин канцелярии — {_currentUser.Role}";

        txtName.Text = _currentUser.FullName;
        txtEmail.Text = _currentUser.Email;
        txtPhone.Text = _currentUser.Phone;

        LoadCatalog();
        ApplyRolePermissions();
    }

    private void LoadCatalog()
    {
        var sort = cbSort.SelectedIndex switch
        {
            1 => "price_asc",
            2 => "price_desc",
            _ => "name"
        };

        var table = _productService.GetCatalog(txtSearch.Text.Trim(), sort);
        catalogGrid.DataSource = table;
    }

    private void CatalogGrid_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0 || e.RowIndex >= catalogGrid.Rows.Count) return;
        if (catalogGrid.Rows[e.RowIndex].DataBoundItem is not DataRowView rowView) return;
        var row = rowView.Row;

        if (row.Field<decimal>("Price") > 1000)
        {
            catalogGrid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
            catalogGrid.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(catalogGrid.Font, FontStyle.Bold);
        }

        if (row["OldPrice"] != DBNull.Value && row["DiscountPercent"] != DBNull.Value)
        {
            var oldPrice = Convert.ToDecimal(row["OldPrice"]);
            var discount = Convert.ToInt32(row["DiscountPercent"]);
            var finalPrice = Math.Round(oldPrice * (1m - discount / 100m), 2);
            catalogGrid.Rows[e.RowIndex].Cells[2].Value = finalPrice;
            catalogGrid.Rows[e.RowIndex].Cells[3].Style.Font = new Font(catalogGrid.Font, FontStyle.Strikeout);
        }
    }

    private int GetSelectedProductId()
    {
        if (catalogGrid.CurrentRow?.Cells[0].Value is null)
            throw new InvalidOperationException("Выберите товар.");

        return Convert.ToInt32(catalogGrid.CurrentRow.Cells[0].Value);
    }

    private void OpenProductEditor(int productId = 0)
    {
        using var form = new ProductEditForm(productId);
        if (form.ShowDialog() == DialogResult.OK)
            LoadCatalog();
    }

    private void DeleteSelectedProduct()
    {
        _productService.Delete(GetSelectedProductId());
        LoadCatalog();
    }

    private void ApplyRolePermissions()
    {
        var canEditProducts = _currentUser.Role is UserRole.Manager or UserRole.Admin;
        var canDelete = _currentUser.Role is UserRole.Admin;

        btnAddProduct.Visible = canEditProducts;
        btnEditProduct.Visible = canEditProducts;
        btnDeleteProduct.Visible = canDelete;
    }

    private void BtnRefresh_Click(object? sender, EventArgs e) => LoadCatalog();
    private void TxtSearch_TextChanged(object? sender, EventArgs e) => LoadCatalog();
    private void CbSort_SelectedIndexChanged(object? sender, EventArgs e) => LoadCatalog();

    private void BtnAddProduct_Click(object? sender, EventArgs e)
    {
        try { OpenProductEditor(); }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private void BtnEditProduct_Click(object? sender, EventArgs e)
    {
        try { OpenProductEditor(GetSelectedProductId()); }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private void BtnDeleteProduct_Click(object? sender, EventArgs e)
    {
        try { DeleteSelectedProduct(); }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void BtnSaveChanges_Click(object? sender, EventArgs e)
    {
        try
        {
            _userService.UpdateProfile(_currentUser.UserId, txtName.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim());
            MessageBox.Show("Профиль сохранён.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}

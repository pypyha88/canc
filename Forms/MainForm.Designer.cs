namespace StationeryStore.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;

    private TabControl tabs = null!;
    private TabPage tabCatalog = null!;
    private TabPage tabProfile = null!;

    private DataGridView catalogGrid = null!;
    private TextBox txtSearch = null!;
    private ComboBox cbSort = null!;
    private Button btnRefresh = null!;
    private Button btnAddProduct = null!;
    private Button btnEditProduct = null!;
    private Button btnDeleteProduct = null!;

    private TextBox txtName = null!;
    private TextBox txtEmail = null!;
    private TextBox txtPhone = null!;
    private Button btnSaveChanges = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        tabs = new TabControl();
        tabCatalog = new TabPage();
        tabProfile = new TabPage();

        catalogGrid = new DataGridView();
        txtSearch = new TextBox();
        cbSort = new ComboBox();
        btnRefresh = new Button();
        btnAddProduct = new Button();
        btnEditProduct = new Button();
        btnDeleteProduct = new Button();

        txtName = new TextBox();
        txtEmail = new TextBox();
        txtPhone = new TextBox();
        btnSaveChanges = new Button();

        var topPanel = new FlowLayoutPanel();
        var profileLayout = new TableLayoutPanel();

        ((System.ComponentModel.ISupportInitialize)catalogGrid).BeginInit();
        SuspendLayout();

        WindowState = FormWindowState.Maximized;

        tabs.Dock = DockStyle.Fill;
        tabs.Controls.Add(tabCatalog);
        tabs.Controls.Add(tabProfile);

        tabCatalog.Text = "Каталог";
        tabProfile.Text = "Личный кабинет";

        txtSearch.PlaceholderText = "Поиск";
        txtSearch.TextChanged += TxtSearch_TextChanged;

        cbSort.DropDownStyle = ComboBoxStyle.DropDownList;
        cbSort.Items.AddRange(new object[] { "По названию", "Сначала дешевые", "Сначала дорогие" });
        cbSort.SelectedIndex = 0;
        cbSort.SelectedIndexChanged += CbSort_SelectedIndexChanged;

        btnRefresh.Text = "Обновить";
        btnRefresh.Click += BtnRefresh_Click;

        btnAddProduct.Text = "Добавить товар";
        btnAddProduct.Click += BtnAddProduct_Click;

        btnEditProduct.Text = "Редактировать";
        btnEditProduct.Click += BtnEditProduct_Click;

        btnDeleteProduct.Text = "Удалить";
        btnDeleteProduct.Click += BtnDeleteProduct_Click;

        topPanel.Dock = DockStyle.Top;
        topPanel.Height = 40;
        topPanel.Controls.AddRange(new Control[] { txtSearch, cbSort, btnRefresh, btnAddProduct, btnEditProduct, btnDeleteProduct });

        catalogGrid.Dock = DockStyle.Fill;
        catalogGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        catalogGrid.CellFormatting += CatalogGrid_CellFormatting;

        tabCatalog.Controls.Add(catalogGrid);
        tabCatalog.Controls.Add(topPanel);

        profileLayout.Dock = DockStyle.Top;
        profileLayout.Height = 180;
        profileLayout.Padding = new Padding(20);
        profileLayout.RowCount = 8;
        profileLayout.ColumnCount = 1;

        btnSaveChanges.Name = "btnSaveChanges";
        btnSaveChanges.Text = "Сохранить профиль";
        btnSaveChanges.Click += BtnSaveChanges_Click;

        profileLayout.Controls.Add(new Label { Text = "Имя" });
        profileLayout.Controls.Add(txtName);
        profileLayout.Controls.Add(new Label { Text = "Email" });
        profileLayout.Controls.Add(txtEmail);
        profileLayout.Controls.Add(new Label { Text = "Телефон" });
        profileLayout.Controls.Add(txtPhone);
        profileLayout.Controls.Add(btnSaveChanges);

        tabProfile.Controls.Add(profileLayout);

        Controls.Add(tabs);
        ((System.ComponentModel.ISupportInitialize)catalogGrid).EndInit();
        ResumeLayout(false);
    }
}

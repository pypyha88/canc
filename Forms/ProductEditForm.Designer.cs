namespace StationeryStore.Forms;

partial class ProductEditForm
{
    private System.ComponentModel.IContainer? components = null;
    private TextBox txtName = null!;
    private NumericUpDown numPrice = null!;
    private NumericUpDown numOldPrice = null!;
    private NumericUpDown numDiscount = null!;
    private NumericUpDown numStock = null!;
    private PictureBox picturePreview = null!;
    private Button btnLoadImage = null!;
    private Button btnSave = null!;

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
        txtName = new TextBox();
        numPrice = new NumericUpDown();
        numOldPrice = new NumericUpDown();
        numDiscount = new NumericUpDown();
        numStock = new NumericUpDown();
        picturePreview = new PictureBox();
        btnLoadImage = new Button();
        btnSave = new Button();
        var layout = new TableLayoutPanel();

        ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numOldPrice).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numDiscount).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numStock).BeginInit();
        ((System.ComponentModel.ISupportInitialize)picturePreview).BeginInit();

        SuspendLayout();

        Width = 400;
        Height = 480;
        StartPosition = FormStartPosition.CenterParent;

        layout.Dock = DockStyle.Fill;
        layout.Padding = new Padding(16);
        layout.RowCount = 12;
        layout.ColumnCount = 1;

        numPrice.Maximum = 1_000_000;
        numPrice.DecimalPlaces = 2;
        numOldPrice.Maximum = 1_000_000;
        numOldPrice.DecimalPlaces = 2;
        numDiscount.Maximum = 100;
        numStock.Maximum = 10_000;

        picturePreview.Height = 120;
        picturePreview.Dock = DockStyle.Top;
        picturePreview.SizeMode = PictureBoxSizeMode.Zoom;

        btnLoadImage.Text = "Загрузить изображение";
        btnLoadImage.Click += BtnLoadImage_Click;

        btnSave.Text = "Сохранить";
        btnSave.Click += BtnSave_Click;

        layout.Controls.Add(txtName);
        layout.Controls.Add(new Label { Text = "Цена" });
        layout.Controls.Add(numPrice);
        layout.Controls.Add(new Label { Text = "Старая цена" });
        layout.Controls.Add(numOldPrice);
        layout.Controls.Add(new Label { Text = "Скидка, %" });
        layout.Controls.Add(numDiscount);
        layout.Controls.Add(new Label { Text = "Остаток" });
        layout.Controls.Add(numStock);
        layout.Controls.Add(picturePreview);
        layout.Controls.Add(btnLoadImage);
        layout.Controls.Add(btnSave);

        Controls.Add(layout);

        ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
        ((System.ComponentModel.ISupportInitialize)numOldPrice).EndInit();
        ((System.ComponentModel.ISupportInitialize)numDiscount).EndInit();
        ((System.ComponentModel.ISupportInitialize)numStock).EndInit();
        ((System.ComponentModel.ISupportInitialize)picturePreview).EndInit();

        ResumeLayout(false);
    }
}

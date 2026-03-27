namespace StationeryStore.Forms;

partial class RegisterForm
{
    private System.ComponentModel.IContainer? components = null;
    private TextBox txtFullName = null!;
    private TextBox txtEmail = null!;
    private TextBox txtPhone = null!;
    private TextBox txtPassword = null!;
    private Button btnRegister = null!;

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
        txtFullName = new TextBox();
        txtEmail = new TextBox();
        txtPhone = new TextBox();
        txtPassword = new TextBox();
        btnRegister = new Button();
        var layout = new TableLayoutPanel();
        SuspendLayout();

        Text = "Регистрация";
        Width = 430;
        Height = 280;
        StartPosition = FormStartPosition.CenterParent;

        layout.Dock = DockStyle.Fill;
        layout.Padding = new Padding(16);
        layout.RowCount = 10;
        layout.ColumnCount = 1;

        txtPassword.UseSystemPasswordChar = true;

        btnRegister.Text = "Создать аккаунт";
        btnRegister.Name = "btnRegister";
        btnRegister.Click += BtnRegister_Click;

        layout.Controls.Add(txtFullName);
        layout.Controls.Add(txtEmail);
        layout.Controls.Add(txtPhone);
        layout.Controls.Add(txtPassword);
        layout.Controls.Add(btnRegister);

        Controls.Add(layout);
        ResumeLayout(false);
    }
}

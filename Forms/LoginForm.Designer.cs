namespace StationeryStore.Forms;

partial class LoginForm
{
    private System.ComponentModel.IContainer? components = null;
    private TextBox txtLogin = null!;
    private TextBox txtPassword = null!;
    private Button btnLogin = null!;
    private Button btnOpenRegister = null!;

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
        txtLogin = new TextBox();
        txtPassword = new TextBox();
        btnLogin = new Button();
        btnOpenRegister = new Button();
        var lblLogin = new Label();
        var lblPassword = new Label();
        var layout = new TableLayoutPanel();
        var actions = new FlowLayoutPanel();
        SuspendLayout();

        Text = "Магазин канцелярии — Вход";
        Width = 400;
        Height = 230;
        StartPosition = FormStartPosition.CenterScreen;

        layout.Dock = DockStyle.Fill;
        layout.Padding = new Padding(16);
        layout.RowCount = 5;
        layout.ColumnCount = 1;

        lblLogin.Text = "Логин (email или телефон)";
        lblPassword.Text = "Пароль";

        txtPassword.UseSystemPasswordChar = true;

        btnLogin.Text = "Войти";
        btnLogin.Name = "btnLogin";
        btnLogin.Click += BtnLogin_Click;

        btnOpenRegister.Text = "Регистрация";
        btnOpenRegister.Name = "btnOpenRegister";
        btnOpenRegister.Click += BtnOpenRegister_Click;

        actions.Dock = DockStyle.Fill;
        actions.Controls.Add(btnLogin);
        actions.Controls.Add(btnOpenRegister);

        layout.Controls.Add(lblLogin);
        layout.Controls.Add(txtLogin);
        layout.Controls.Add(lblPassword);
        layout.Controls.Add(txtPassword);
        layout.Controls.Add(actions);

        Controls.Add(layout);
        ResumeLayout(false);
    }
}

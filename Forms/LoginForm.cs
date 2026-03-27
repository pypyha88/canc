using StationeryStore.Services;

namespace StationeryStore.Forms;

public class LoginForm : Form
{
    private readonly TextBox _txtLogin = new() { PlaceholderText = "Email или телефон" };
    private readonly TextBox _txtPassword = new() { UseSystemPasswordChar = true, PlaceholderText = "Пароль" };
    private readonly Button _btnLogin = new() { Text = "Войти", Name = "btnLogin" };
    private readonly Button _btnRegister = new() { Text = "Регистрация", Name = "btnOpenRegister" };
    private readonly UserService _userService = new();

    public LoginForm()
    {
        Text = "Магазин канцелярии — Вход";
        Width = 400;
        Height = 230;
        StartPosition = FormStartPosition.CenterScreen;

        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), RowCount = 5, ColumnCount = 1 };
        layout.Controls.Add(new Label { Text = "Логин" });
        layout.Controls.Add(_txtLogin);
        layout.Controls.Add(new Label { Text = "Пароль" });
        layout.Controls.Add(_txtPassword);

        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill };
        actions.Controls.Add(_btnLogin);
        actions.Controls.Add(_btnRegister);
        layout.Controls.Add(actions);
        Controls.Add(layout);

        _btnLogin.Click += BtnLogin_Click;
        _btnRegister.Click += (_, _) => new RegisterForm().ShowDialog();
    }

    private void BtnLogin_Click(object? sender, EventArgs e)
    {
        try
        {
            var user = _userService.Login(_txtLogin.Text.Trim(), _txtPassword.Text);
            if (user is null)
            {
                MessageBox.Show("Неверный логин или пароль.");
                return;
            }

            Hide();
            new MainForm(user).ShowDialog();
            Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

using StationeryStore.Services;

namespace StationeryStore.Forms;

public class RegisterForm : Form
{
    private readonly TextBox _txtFullName = new() { PlaceholderText = "ФИО" };
    private readonly TextBox _txtEmail = new() { PlaceholderText = "Email" };
    private readonly TextBox _txtPhone = new() { PlaceholderText = "Телефон" };
    private readonly TextBox _txtPassword = new() { PlaceholderText = "Пароль", UseSystemPasswordChar = true };
    private readonly Button _btnRegister = new() { Text = "Создать аккаунт", Name = "btnRegister" };
    private readonly UserService _userService = new();

    public RegisterForm()
    {
        Text = "Регистрация";
        Width = 430;
        Height = 280;
        StartPosition = FormStartPosition.CenterParent;

        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), RowCount = 10, ColumnCount = 1 };
        layout.Controls.Add(_txtFullName);
        layout.Controls.Add(_txtEmail);
        layout.Controls.Add(_txtPhone);
        layout.Controls.Add(_txtPassword);
        layout.Controls.Add(_btnRegister);
        Controls.Add(layout);

        _btnRegister.Click += (_, _) =>
        {
            try
            {
                _userService.Register(_txtFullName.Text.Trim(), _txtEmail.Text.Trim(), _txtPhone.Text.Trim(), _txtPassword.Text);
                MessageBox.Show("Регистрация прошла успешно.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };
    }
}

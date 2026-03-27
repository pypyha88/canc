using StationeryStore.Services;

namespace StationeryStore.Forms;

public partial class LoginForm : Form
{
    private readonly UserService _userService = new();

    public LoginForm()
    {
        InitializeComponent();
    }

    private void BtnLogin_Click(object? sender, EventArgs e)
    {
        try
        {
            var user = _userService.Login(txtLogin.Text.Trim(), txtPassword.Text);
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

    private void BtnOpenRegister_Click(object? sender, EventArgs e)
    {
        new RegisterForm().ShowDialog();
    }
}

using StationeryStore.Services;

namespace StationeryStore.Forms;

public partial class RegisterForm : Form
{
    private readonly UserService _userService = new();

    public RegisterForm()
    {
        InitializeComponent();
    }

    private void BtnRegister_Click(object? sender, EventArgs e)
    {
        try
        {
            _userService.Register(txtFullName.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), txtPassword.Text);
            MessageBox.Show("Регистрация прошла успешно.");
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

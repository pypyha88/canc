using System.Text.RegularExpressions;
using StationeryStore.Data;
using StationeryStore.Forms;
using StationeryStore.Models;

namespace StationeryStore;

internal static class Program
{
    public static AppConfig Config { get; private set; } = new AppConfig();

    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        LoadConfig();

        Application.ThreadException += (_, e) =>
            MessageBox.Show($"Произошла ошибка: {e.Exception.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            MessageBox.Show($"Критическая ошибка: {(e.ExceptionObject as Exception)?.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

        Application.Run(new LoginForm());
    }

    private static void LoadConfig()
    {
        var configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!File.Exists(configPath))
        {
            configPath = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
        }

        if (!File.Exists(configPath))
        {
            throw new FileNotFoundException("Файл appsettings.json не найден.");
        }

        var json = File.ReadAllText(configPath);
        var match = Regex.Match(json, "\"DefaultConnection\"\\s*:\\s*\"(?<cs>[^\"]+)\"");
        if (!match.Success)
        {
            throw new InvalidOperationException("Не найдена строка подключения DefaultConnection в appsettings.json");
        }

        Config = new AppConfig
        {
            ConnectionStrings = new ConnectionStrings
            {
                DefaultConnection = match.Groups["cs"].Value
            }
        };
    }
}

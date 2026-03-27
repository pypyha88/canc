using System.Text.Json;
using StationeryStore.Data;
using StationeryStore.Forms;
using StationeryStore.Models;

namespace StationeryStore;

internal static class Program
{
    public static AppConfig Config { get; private set; } = new();

    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
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
        Config = JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
    }
}

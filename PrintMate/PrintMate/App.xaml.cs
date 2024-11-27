using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;
using System.Windows;

namespace PrintMate;

public partial class App : Application
{
    // enable console for debuggin
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool AllocConsole();

    public IServiceProvider ServiceProvider { get; private set; }

    public App()
    {
        InitializeComponent();
        AllocConsole(); // enables console
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ServiceCollection serviceCollection = new();
        ConfigureServices(serviceCollection);
        ServiceProvider = serviceCollection.BuildServiceProvider();
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
    }
}

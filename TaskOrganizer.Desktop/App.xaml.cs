using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;
using TaskOrganizer.Desktop.Pages.Login;
using TaskOrganizer.Desktop.Pages.Signup;
using TaskOrganizer.Desktop.Services;
using TaskOrganizer.Desktop.Windows;

namespace TaskOrganizer.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
  public IServiceProvider? ServiceProvider { get; private set; }
  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);

    var serviceCollection = new ServiceCollection();
    ConfigureServices(serviceCollection);

    ServiceProvider = serviceCollection.BuildServiceProvider();

    var currentWindow = ServiceProvider.GetRequiredService<AuthWindow>();
    currentWindow.Show();
  }

  private void ConfigureServices(IServiceCollection services)
  {
    // Services
    services.AddSingleton<AuthService>();

    // Windows
    services.AddTransient<AuthWindow>();

    // Pages
    services.AddTransient<LoginPage>();
    services.AddTransient<LoginPageVM>();
    services.AddTransient<SignupPage>();
    services.AddTransient<SignupPageVM>();
  }
}


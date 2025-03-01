using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;
using TaskOrganizer.Desktop.Windows;

namespace TaskOrganizer.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);
    var currentWindow = new AuthWindow();
    currentWindow.Show();
  }
}


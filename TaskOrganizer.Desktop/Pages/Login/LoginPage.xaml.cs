using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskOrganizer.Desktop.Pages.Signup;
using TaskOrganizer.Desktop.Services;
using TaskOrganizer.Desktop.Windows;

namespace TaskOrganizer.Desktop.Pages.Login
{
  /// <summary>
  /// Interaction logic for Login.xaml
  /// </summary>
  public partial class LoginPage : Page
  {
    private IServiceProvider _serviceProvider;
    private AuthService _authService;
    public LoginPage(IServiceProvider serviceProvider, AuthService authService, LoginPageVM vm)
    {
      InitializeComponent();

      _serviceProvider = serviceProvider;
      _authService = authService;

      DataContext = vm;

      vm.LoginSuccessful += OnLoginSuccessful;
      vm.LoginFailed += OnLoginFailed;
    }

    private void NavigateToSignup_Click(object sender, RoutedEventArgs e)
    {
      this.NavigationService.Navigate(_serviceProvider?.GetRequiredService<SignupPage>());
    }

    private void OnLoginSuccessful(object? sender, EventArgs e)
    {
      Window containerWindow = Window.GetWindow(this);
      if(containerWindow is not null)
      {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
        containerWindow.Close();
      }
    }

    private void OnLoginFailed(object? sender, EventArgs e)
    {
      LoadingScreen.Visibility = Visibility.Collapsed;
      LoginForm.Visibility = Visibility.Visible;
      MessageBox.Show("Login failed");
    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
      LoadingScreen.Visibility = Visibility.Visible;
      LoginForm.Visibility = Visibility.Collapsed;
    }
  }
}

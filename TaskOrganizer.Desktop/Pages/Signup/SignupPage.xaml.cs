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
using TaskOrganizer.Desktop.Pages.Login;

namespace TaskOrganizer.Desktop.Pages.Signup
{
  /// <summary>
  /// Interaction logic for Signup.xaml
  /// </summary>
  public partial class SignupPage : Page
  {
    IServiceProvider? _serviceProvider;
    public SignupPage(IServiceProvider serviceProvider, SignupPageVM vm)
    {
      InitializeComponent();
      _serviceProvider = serviceProvider;
      DataContext = vm;

      vm.SignUpStarted += SignUpStarted;
      vm.SignUpSuccessful += OnSignUpSuccessful;
      vm.SignUpFailed += OnSignUpFailed;
    }

    private void SignUpStarted(object? sender, object? e)
    {
      LoadingScreen.Visibility = Visibility.Visible;
      SignupForm.Visibility = Visibility.Collapsed;
    }

    private void OnSignUpSuccessful(object? sender, string e)
    {
      this.NavigationService.Navigate(this._serviceProvider?.GetRequiredService<LoginPage>());
      MessageBox.Show($"User {e} created!");
    }

    private void OnSignUpFailed(object? sender, object? e)
    {
      LoadingScreen.Visibility = Visibility.Collapsed;
      SignupForm.Visibility = Visibility.Visible;
      MessageBox.Show($"Sign up failed!");
    }

    private void NavigateToLogin_Click(object sender, RoutedEventArgs e)
    {
      this.NavigationService.Navigate(this._serviceProvider?.GetRequiredService<LoginPage>());
    }
  }
}

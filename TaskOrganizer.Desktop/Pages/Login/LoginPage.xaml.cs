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

namespace TaskOrganizer.Desktop.Pages.Login
{
  /// <summary>
  /// Interaction logic for Login.xaml
  /// </summary>
  public partial class LoginPage : Page
  {
    IServiceProvider? _serviceProvider;
    public LoginPage(IServiceProvider serviceProvider, LoginPageVM vm)
    {
      InitializeComponent();
      _serviceProvider = serviceProvider;
      DataContext = vm;
    }

    private void NavigateToSignup_Click(object sender, RoutedEventArgs e)
    {
      this.NavigationService.Navigate(_serviceProvider?.GetRequiredService<SignupPage>());
    }
  }
}

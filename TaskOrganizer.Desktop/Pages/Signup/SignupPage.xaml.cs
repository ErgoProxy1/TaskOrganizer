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
    public SignupPage()
    {
      InitializeComponent();
      DataContext = new SignupPageVM();
    }

    private void NavigateToLogin_Click(object sender, RoutedEventArgs e)
    {
      this.NavigationService.Navigate(new LoginPage());
    }
  }
}

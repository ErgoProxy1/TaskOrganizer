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
using System.Windows.Shapes;
using TaskOrganizer.Desktop.Pages.Login;
using TaskOrganizer.Desktop.Pages.Signup;

namespace TaskOrganizer.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
  public partial class AuthWindow : Window
  {
    public AuthWindow(LoginPage loginPage)
    {
      InitializeComponent();
      AuthFrame.Navigate(loginPage);
    }
  }
}

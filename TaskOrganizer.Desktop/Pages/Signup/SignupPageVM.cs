using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskOrganizer.Desktop.Interfaces;

namespace TaskOrganizer.Desktop.Pages.Signup
{
  class SignupPageVM : BaseViewModel
  {

    private string _username = string.Empty;
    public string Username
    {
      get => _username;
      set
      {
        _username = value;
        OnPropertyChanged(value);
      }
    }

    private string _email = string.Empty;
    public string Email
    {
      get => _email;
      set
      {
        _email = value;
        OnPropertyChanged(value);
      }
    }

    private string _password = string.Empty;
    public string Password
    {
      get => _password;
      set
      {
        _password = value;
        OnPropertyChanged(value);
      }
    }

    private string _confirmPassword = string.Empty;
    public string ConfirmPassword
    {
      get => _confirmPassword;
      set
      {
        _confirmPassword = value;
        OnPropertyChanged(value);
      }
    }
  }
}

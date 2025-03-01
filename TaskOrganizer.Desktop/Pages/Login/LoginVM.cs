using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TaskOrganizer.Desktop.Pages.Login
{
  class LoginVM : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler? PropertyChanged;

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

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
  }
}

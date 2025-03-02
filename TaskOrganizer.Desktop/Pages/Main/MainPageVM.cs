using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrganizer.Desktop.Helper;
using TaskOrganizer.Desktop.Services;

namespace TaskOrganizer.Desktop.Pages.Main
{
  public class MainPageVM : BaseViewModel
  {
    private AuthService _auth;
    public MainPageVM(AuthService auth)
    {
      _auth = auth;
    }
    public string Username 
    {
      get => _auth.CurrentUser?.Username ?? string.Empty;
    }

    public string Email
    {
      get => _auth.CurrentUser?.Email ?? string.Empty;
    }
  }
}

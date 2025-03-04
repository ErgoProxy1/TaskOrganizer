using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrganizer.API.Models;

namespace TaskOrganizer.Desktop.Services
{
  public class AuthService
  {
    public UserModel? CurrentUser { get; private set; }

    public void SetCurrentUser(UserModel? user)
    {
      if (user is not null)
      {
        CurrentUser = user; 
      }
    }
  }
}

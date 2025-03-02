using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrganizer.API.Models;

namespace TaskOrganizer.Desktop.Services
{
  class AuthService
  {
    public User? CurrentUser { get; private set; }

    public void SetCurrentUser(User user)
    {
      CurrentUser = user;
    }
  }
}

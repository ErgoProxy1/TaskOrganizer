
using TaskOrganizer.API.DTOs;

namespace TaskOrganizer.Desktop.Services
{
  public class AuthService
  {
    public UserResponseDTO? CurrentUser { get; private set; }

    public void SetCurrentUser(UserResponseDTO? user)
    {
      if (user is not null)
      {
        CurrentUser = user; 
      }
    }
  }
}

namespace TaskOrganizer.API.Contracts
{
  public class LoginContract
  {
    // Lowercase becase FireAuth expects it
    public string email = string.Empty;
    public string password = string.Empty;
    public bool returnSecureToken;
  }
}

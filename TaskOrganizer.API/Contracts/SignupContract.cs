namespace TaskOrganizer.API.Contracts
{
  public class SignupContract
  {
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
  }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskOrganizer.API.Contracts;
using TaskOrganizer.Desktop.Helper;
using TaskOrganizer.Desktop.Interfaces;

namespace TaskOrganizer.Desktop.Pages.Signup
{
  public class SignupPageVM : BaseViewModel
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

    public event EventHandler<string>? SignUpSuccessful;

    public ICommand CreateUserCommand { get; }
    public SignupPageVM() 
    {
      CreateUserCommand = new RelayCommand(async () => await CreateUserWithEmailAndPassword());
    }

    private async Task CreateUserWithEmailAndPassword()
    {
      using (HttpClient client = new HttpClient())
      {
        SignupContract requestBody = new SignupContract { Email = Email, Password = Password, Username = Username };
        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("http://localhost:5056/api/auth/create-user", content);
        if (response.IsSuccessStatusCode)
        {
          SignUpSuccessful?.Invoke(this, Username);
        }
      }
    }
  }
}

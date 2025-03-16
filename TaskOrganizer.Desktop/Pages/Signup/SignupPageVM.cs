using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskOrganizer.API.DTOs;
using TaskOrganizer.Desktop.Helper;

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

    public event EventHandler<string>? SignUpSuccessful;
    public event EventHandler? SignUpFailed;
    public event EventHandler? SignUpStarted;

    public ICommand CreateUserCommand { get; }
    public SignupPageVM() 
    {
      CreateUserCommand = new AsyncRelayCommand<object[]>(CreateUserWithEmailAndPassword);
    }

    private async Task CreateUserWithEmailAndPassword(object[]? passwordBoxes)
    {
      var passwordBox = passwordBoxes?[0] is not null ? passwordBoxes[0] as PasswordBox : null;
      var confirmPasswordBox = passwordBoxes?[1] is not null ? passwordBoxes[1] as PasswordBox : null;
      if (passwordBox is not null && confirmPasswordBox is not null && passwordBox?.Password == confirmPasswordBox?.Password)
      {
        SignUpStarted?.Invoke(this, EventArgs.Empty);
        using (HttpClient client = new HttpClient())
        {
          SignupRequest requestBody = new SignupRequest { Email = Email, Password = passwordBox?.Password ?? string.Empty, Username = Username };
          passwordBox?.Clear();
          confirmPasswordBox?.Clear();
          var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
          var response = await client.PostAsync("https://localhost:5056/api/auth/create-user", content);
          if (response.IsSuccessStatusCode)
          {
            SignUpSuccessful?.Invoke(this, Username);
          }
          else
          {
            SignUpFailed?.Invoke(this, EventArgs.Empty);
          }
        } 
      } 
      else
      {
        MessageBox.Show("Passwords did not match!");
        passwordBox?.Clear();
        confirmPasswordBox?.Clear();
      }
    }
  }
}

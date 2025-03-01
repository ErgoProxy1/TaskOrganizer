using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskOrganizer.API.Contracts;
using TaskOrganizer.Desktop.Interfaces;

namespace TaskOrganizer.Desktop.Pages.Login
{
  public class LoginPageVM : BaseViewModel
  {
    private const string FirebaseApiKey = "AIzaSyCWbHn1uBiby3RPHRKnvQHuXn4ld7SwAn0"; // Not a secret
    private const string SignInUrl = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + FirebaseApiKey;

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

    public ICommand LoginCommand { get; }

    public LoginPageVM()
    {
      LoginCommand = new RelayCommand(async () => await SignInWithEmailAndPassword());
    }

    private async Task SignInWithEmailAndPassword()
    {
      using (HttpClient client = new HttpClient())
      {
        var requestBody = new
        {
          email = Email,
          password = Password,
          returnSecureToken = true
        };

        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(SignInUrl, content);

        if (response.IsSuccessStatusCode)
        {
          var responseBody = await response.Content.ReadAsStringAsync();
          var authResponse = JsonConvert.DeserializeObject<LoginContract>(responseBody);
          if (authResponse is not null)
          {
            string idToken = authResponse.IdToken;

            Debug.WriteLine("User signed in successfully!");
            Debug.WriteLine($"ID Token: {idToken}");

            // Send ID token to backend
            await SendIdTokenToBackend(idToken); 
          }
          else
          {
            Debug.WriteLine("Auth token was null.");
          }
        }
        else
        {
          Debug.WriteLine("Failed to sign in.");
        }
      }
    }

    private async Task SendIdTokenToBackend(string idToken)
    {
      using (HttpClient client = new HttpClient())
      {
        var content = new StringContent(JsonConvert.SerializeObject(new LoginContract(){ IdToken = idToken }), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("http://localhost:5056/api/auth/verify-token", content);

        if (response.IsSuccessStatusCode)
        {
          Debug.WriteLine("Token sent to backend successfully!");
        }
        else
        {
          Debug.WriteLine("Failed to send token to backend.");
        }
      }
    }
  }
}

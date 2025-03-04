﻿using CommunityToolkit.Mvvm.Input;
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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using TaskOrganizer.API.Contracts;
using TaskOrganizer.Desktop.Helper;
using TaskOrganizer.API.Models;
using TaskOrganizer.Desktop.Services;

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

    public event EventHandler? LoginSuccessful;
    public event EventHandler? LoginFailed;

    public ICommand LoginCommand { get; }

    private AuthService _authService;
    public LoginPageVM(AuthService authService)
    {
      _authService = authService;
      LoginCommand = new AsyncRelayCommand<object>(SignInWithEmailAndPassword);
    }

    private async Task SignInWithEmailAndPassword(object? passwordBox)
    {
      using (HttpClient client = new HttpClient())
      {
        var wPasswordBox = passwordBox as PasswordBox;
        var requestBody = new // This is a standardized object expected by firebase auth
        {
          email = Email,
          password = wPasswordBox?.Password ?? string.Empty,
          returnSecureToken = true
        };
        wPasswordBox?.Clear();

        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(SignInUrl, content);

        if (response.IsSuccessStatusCode)
        {
          var responseBody = await response.Content.ReadAsStringAsync();
          var authResponse = JsonConvert.DeserializeObject<VerifyTokenRequest>(responseBody);
          if (authResponse is not null)
          {
            string idToken = authResponse.IdToken;
            await SendIdTokenToBackend(idToken); 
          }
        }
        else
        {
          LoginFailed?.Invoke(this, EventArgs.Empty);
        }
      }
    }

    private async Task SendIdTokenToBackend(string idToken)
    {
      using (HttpClient client = new HttpClient())
      {
        var content = new StringContent(JsonConvert.SerializeObject(new VerifyTokenRequest(){ IdToken = idToken }), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("http://localhost:5056/api/auth/verify-token", content);

        if (response.IsSuccessStatusCode)
        {
          var responseBody = await response.Content.ReadAsStringAsync();
          var userResponse = JsonConvert.DeserializeObject<UserModel>(responseBody);
          _authService.SetCurrentUser(userResponse);
          LoginSuccessful?.Invoke(this, EventArgs.Empty);
        }
        else
        {
          LoginFailed?.Invoke(this, EventArgs.Empty);
        }
      }
    }
  }
}

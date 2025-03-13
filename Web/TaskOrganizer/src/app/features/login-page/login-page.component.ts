import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { take } from 'rxjs';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout'
import { TuiAppearance, TuiButton, TuiError, TuiIcon, TuiNotification, TuiTextfield, TuiTitle } from '@taiga-ui/core';
import { MatDividerModule } from '@angular/material/divider'

@Component({
  selector: 'app-login-page',
  imports: [ReactiveFormsModule, TuiCardLarge, TuiButton, TuiError, TuiIcon, TuiNotification, TuiTextfield, TuiTitle, TuiAppearance, TuiHeader, MatDividerModule],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginPageComponent {
  private readonly firebaseApiKey = "AIzaSyCWbHn1uBiby3RPHRKnvQHuXn4ld7SwAn0"; // Not a secret
  //private readonly signInUrl = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + this.firebaseApiKey;
  private readonly signInUrl = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=a"

  private fb = inject(FormBuilder);
  private http = inject(HttpClient);

  protected loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  })

  protected login(): void {
    let requestBody = {
      email: this.loginForm.controls.email.value,
      password: this.loginForm.controls.password.value,
      returnSecureToken: true
    }

    let headers = {headers: new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'})};
    this.http.post(this.signInUrl, JSON.stringify(requestBody), headers).pipe(take(1)).subscribe(
      (response) => { 
        console.log(response)
      },
      (error: HttpErrorResponse) => {
        console.log(error)
      } 
    )
  }
}

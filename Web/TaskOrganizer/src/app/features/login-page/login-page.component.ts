import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { take } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login-page',
  imports: [MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatDividerModule, ReactiveFormsModule],
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

  private snackBar = inject(MatSnackBar);

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

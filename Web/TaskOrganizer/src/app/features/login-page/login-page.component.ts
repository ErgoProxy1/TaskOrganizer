import { ChangeDetectionStrategy, Component, DestroyRef, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { TuiCardLarge, TuiHeader } from '@taiga-ui/layout'
import { TuiAlertService, TuiAppearance, TuiButton, TuiError, TuiTextfield, TuiTitle } from '@taiga-ui/core';
import { MatDividerModule } from '@angular/material/divider'
import { AuthService } from '../../core/services/auth.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop'
import { take } from 'rxjs';
import { AuthError, AuthFirebaseSignin } from '../../core/models/auth.models';

@Component({
  selector: 'app-login-page',
  imports: [ReactiveFormsModule, TuiCardLarge, TuiButton, TuiError, TuiTextfield, TuiTitle, TuiAppearance, TuiHeader, MatDividerModule],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginPageComponent {
  private fb = inject(FormBuilder);
  private auth = inject(AuthService);
  private destroyRef = inject(DestroyRef);
  private tuiAlert = inject(TuiAlertService);

  protected loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  })

  protected submit(): void {
    let requestBody: AuthFirebaseSignin = {
      email: this.loginForm.controls.email.value ?? '',
      password: this.loginForm.controls.password.value ?? '',
      returnSecureToken: true
    }
    this.auth.signIn(requestBody).pipe(takeUntilDestroyed(this.destroyRef)).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (error: AuthError) => {
        this.tuiAlert.open(error.message, {autoClose: 3000, icon: error.icon, appearance: 'negative'}).pipe(take(1), takeUntilDestroyed(this.destroyRef)).subscribe();
      }
    });
  }
}

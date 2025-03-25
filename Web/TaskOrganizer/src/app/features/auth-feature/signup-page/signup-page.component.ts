import { ChangeDetectionStrategy, Component, DestroyRef, inject, output, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDividerModule } from '@angular/material/divider';
import { TuiButton, TuiError, TuiTextfield, TuiTitle, TuiAppearance, TuiAlertService, TuiLoader } from '@taiga-ui/core';
import { TuiHeader } from '@taiga-ui/layout';
import { AuthService } from '../../../core/services/auth.service';
import { CustomValidators } from '../../../shared/helpers/custom-validators';
import { HttpClient } from '@angular/common/http';
import { take } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { AuthError } from '../../../core/models/auth.models';
import { SignupRequest } from '../../../core/contracts/backend.contracts';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup-page',
  imports: [ReactiveFormsModule, TuiButton, TuiError, TuiTextfield, TuiTitle, TuiAppearance, TuiHeader, MatDividerModule, TuiLoader],
  templateUrl: './signup-page.component.html',
  styleUrl: './signup-page.component.scss',
})
export class SignupPageComponent {
  public switchPage = output();

  protected loading = signal(false);

  private fb = inject(FormBuilder);
  private destroyRef = inject(DestroyRef);
  private tuiAlert = inject(TuiAlertService);
  private http = inject(HttpClient);
  private router = inject(Router);

  protected signupForm = this.fb.group({
    username: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: [''],
  });

  constructor() {
    this.signupForm.addValidators(CustomValidators.passwordMatch);
  }

  public submit() {
    this.loading.set(true);
    let email = this.signupForm.value.email;
    let password = this.signupForm.value.password;
    let username = this.signupForm.value.username;
    if (email && password && username) {
      let signupRequest: SignupRequest = { email, password, username };
      this.http
        .post('/api/auth/create-user', signupRequest)
        .pipe(take(1), takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: () => {
            this.loading.set(false);
            this.switchPage.emit();
            this.tuiAlert
              .open('Signup successful, please login', { autoClose: 3000, icon: 'check', appearance: 'success' })
              .pipe(take(1))
              .subscribe();
          },
          error: (error: AuthError) => {
            this.showAlert(error);
          },
        });
    }
  }

  private showAlert(error: AuthError) {
    this.loading.set(false);
    this.tuiAlert.open(error.message, { autoClose: 3000, icon: error.icon, appearance: 'negative' }).pipe(take(1)).subscribe();
  }
}

import { ChangeDetectionStrategy, Component, DestroyRef, inject, output, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { TuiHeader } from '@taiga-ui/layout';
import { TuiAlertService, TuiAppearance, TuiButton, TuiError, TuiTextfield, TuiTitle } from '@taiga-ui/core';
import { MatDividerModule } from '@angular/material/divider';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { take } from 'rxjs';
import { AuthService } from '../../../core/services/auth.service';
import { AuthError } from '../../../core/models/auth.models';
import { UserModel } from '../../../core/models/api.models';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  imports: [ReactiveFormsModule, TuiButton, TuiError, TuiTextfield, TuiTitle, TuiAppearance, TuiHeader, MatDividerModule],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginPageComponent {
  public switchPage = output();

  private fb = inject(FormBuilder);
  private auth = inject(AuthService);
  private destroyRef = inject(DestroyRef);
  private tuiAlert = inject(TuiAlertService);
  private router = inject(Router);

  protected loading = signal(false);

  protected loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
  });

  protected submit(): void {
    if (this.loading()) {
      return;
    }
    this.loading.set(true);
    let email = this.loginForm.controls.email.value ?? '';
    let password = this.loginForm.controls.password.value ?? '';
    this.auth
      .login(email, password)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (user) => {
          if (user) {
            this.loading.set(false);
            this.router.navigate(['/tasks']);
          } else {
            this.showAlert(new AuthError('Token error, please try again later', 'shield-x'));
          }
        },
        error: (error: AuthError) => {
          this.showAlert(error);
        },
      });
  }

  private showAlert(error: AuthError) {
    this.loading.set(false);
    this.tuiAlert
      .open(error.message, { autoClose: 3000, icon: error.icon, appearance: 'negative' })
      .pipe(take(1), takeUntilDestroyed(this.destroyRef))
      .subscribe();
  }
}

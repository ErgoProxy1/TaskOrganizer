import { ChangeDetectionStrategy, Component, DestroyRef, inject, output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDividerModule } from '@angular/material/divider';
import { TuiButton, TuiError, TuiTextfield, TuiTitle, TuiAppearance, TuiAlertService } from '@taiga-ui/core';
import { TuiHeader } from '@taiga-ui/layout';
import { AuthService } from '../../../core/services/auth.service';
import { CustomValidators } from '../../../shared/helpers/custom-validators';

@Component({
  selector: 'app-signup-page',
  imports: [ReactiveFormsModule, TuiButton, TuiError, TuiTextfield, TuiTitle, TuiAppearance, TuiHeader, MatDividerModule],
  templateUrl: './signup-page.component.html',
  styleUrl: './signup-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SignupPageComponent {
  public switchPage = output();

  private fb = inject(FormBuilder);
  private auth = inject(AuthService);
  private destroyRef = inject(DestroyRef);
  private tuiAlert = inject(TuiAlertService);

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
    console.log(this.signupForm);
  }
}

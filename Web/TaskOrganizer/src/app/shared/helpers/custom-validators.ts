import { AbstractControl, ValidationErrors, Validators } from '@angular/forms';

export class CustomValidators extends Validators {
  public static passwordMatch(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;

    if (password !== confirmPassword) {
      control.get('confirmPassword')?.setErrors({ passwordMatch: true });
      return { passwordMatch: true };
    }
    return null;
  }
}

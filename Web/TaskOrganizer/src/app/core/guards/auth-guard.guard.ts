import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { TuiAlert, TuiAlertService } from '@taiga-ui/core';
import { take } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const auth = inject(AuthService);
  const router = inject(Router);
  const tuiAlert = inject(TuiAlertService);
  if (!auth.user) {
    router.navigate(['']);
    tuiAlert.open('Please login to view tasks!', { closeable: true, autoClose: 3000 }).pipe(take(1)).subscribe();
    return false;
  }
  return true;
};

import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { TuiAlert, TuiAlertService } from '@taiga-ui/core';
import { map, take } from 'rxjs';
import { Auth, authState } from '@angular/fire/auth';

export const noAuthGuard: CanActivateFn = (route, state) => {
  const auth = inject(Auth);
  const router = inject(Router);

  // Use the user signal or an observable to check auth state
  return authState(auth).pipe(
    map((user) => {
      if (user) {
        return true;
      } else {
        router.navigate(['']);
        return false;
      }
    }),
  );
};

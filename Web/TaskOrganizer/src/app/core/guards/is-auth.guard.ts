import { inject } from '@angular/core';
import { Auth, authState } from '@angular/fire/auth';
import { CanActivateFn, Router } from '@angular/router';
import { map } from 'rxjs';

export const isAuthGuard: CanActivateFn = (route, state) => {
  const auth = inject(Auth);
  const router = inject(Router);

  // Use the user signal or an observable to check auth state
  return authState(auth).pipe(
    map((user) => {
      if (user) {
        router.navigate(['/projects']);
        return false;
      } else {
        return true;
      }
    }),
  );
};

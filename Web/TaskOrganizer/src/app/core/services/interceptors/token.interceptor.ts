import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Auth, authState } from '@angular/fire/auth';
import { switchMap } from 'rxjs';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(Auth);

  return authState(auth).pipe(
    switchMap((user) => {
      // Extract token from the authenticated user
      const token = user ? user.getIdToken() : null;

      const modifiedReq = token
        ? req.clone({
            setHeaders: { Authorization: `Bearer ${token}` },
          })
        : req;

      return next(modifiedReq);
    }),
  );
};

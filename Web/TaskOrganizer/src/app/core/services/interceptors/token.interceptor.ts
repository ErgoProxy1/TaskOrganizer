import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Auth, authState } from '@angular/fire/auth';
import { from, switchMap, take } from 'rxjs';
import { AuthService } from '../auth.service';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(Auth);
  if (auth.currentUser) {
    return from(auth.currentUser.getIdToken()).pipe(
      take(1), // <-------------- Only emit the first value!
      switchMap((token: any) => {
        if (token) {
          const modifiedRequest = req.clone({
            setHeaders: { Authorization: `Bearer ${token}` },
          });
          return next(modifiedRequest);
        }
        return next(req);
      }),
    );
  } else {
    return next(req);
  }
};

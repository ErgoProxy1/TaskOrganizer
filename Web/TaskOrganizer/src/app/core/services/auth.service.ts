import { inject, Injectable, signal } from '@angular/core';
import { Observable, catchError, from, take, tap, throwError } from 'rxjs';
import { AuthError } from '../models/auth.models';
import { HttpErrorResponse } from '@angular/common/http';
import { Auth, User, signInWithEmailAndPassword, createUserWithEmailAndPassword, UserCredential, updateProfile } from '@angular/fire/auth';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private unknownError = new AuthError('Unknown error', 'shield-x');

  private userSignal = signal<User | null>(null); // Signal to hold the user
  public getUserSignal() {
    return this.userSignal;
  }

  private auth = inject(Auth);
  private router = inject(Router);
  constructor() {
    this.auth.onAuthStateChanged((user) => this.userSignal.set(user));
  }

  login(email: string, password: string): Observable<User> {
    return from(signInWithEmailAndPassword(this.auth, email, password).then((res) => res.user)).pipe(
      take(1),
      catchError((err: HttpErrorResponse) => {
        if (err.status === 0) {
          return throwError(() => new AuthError('Could not reach server, please verify your connection', 'server-crash'));
        }
        if (err.status === 400) {
          return throwError(() => new AuthError('Login error, please verify your credentials', 'circle-x'));
        }
        return throwError(() => this.unknownError);
      }),
    );
  }

  getToken(): Observable<string> {
    return from(this.auth.currentUser?.getIdToken() || Promise.resolve('')).pipe(
      take(1),
      catchError((err: HttpErrorResponse) => {
        if ([0, 400, 500].includes(err.status)) {
          return throwError(() => new AuthError('Server issue, please try again later', 'cloud-alert'));
        }
        return throwError(() => this.unknownError);
      }),
    );
  }

  logout(): Observable<void> {
    return from(this.auth.signOut()).pipe(
      take(1),
      tap(() => this.router.navigate([''])),
    );
  }
}

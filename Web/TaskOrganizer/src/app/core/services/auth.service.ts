import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError, Observable, take, throwError } from 'rxjs';
import { AuthError, AuthFirebaseSignin } from '../models/auth.models';
import { VerifyTokenRequest } from '../contracts/backend.contracts';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly firebaseApiKey = 'AIzaSyCWbHn1uBiby3RPHRKnvQHuXn4ld7SwAn0'; // Not a secret
  private readonly signInUrl = 'https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=' + this.firebaseApiKey;

  private unknownError = new AuthError('Unknown error', 'shield-x');

  private http = inject(HttpClient);

  public signIn(requestBody: AuthFirebaseSignin): Observable<Object> {
    let headers = { headers: new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' }) };
    return this.http.post(this.signInUrl, JSON.stringify(requestBody), headers).pipe(
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

  public authenticateIdToken(idToken: string) {
    let headers = { headers: new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' }) };
    let body: VerifyTokenRequest = { IdToken: idToken };
    return this.http.post('api/auth/verify-token', JSON.stringify(body), headers).pipe(
      take(1),
      catchError((err: HttpErrorResponse) => {
        if ([0, 400].includes(err.status)) {
          return throwError(() => new AuthError('Server issue, please try again later', 'cloud-alert'));
        }
        return throwError(() => this.unknownError);
      }),
    );
  }
}

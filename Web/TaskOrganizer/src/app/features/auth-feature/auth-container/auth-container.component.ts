import { Component } from '@angular/core';
import { LoginPageComponent } from '../login-page/login-page.component';
import { SignupPageComponent } from "../signup-page/signup-page.component";

@Component({
  selector: 'app-auth-container',
  imports: [LoginPageComponent, SignupPageComponent],
  templateUrl: './auth-container.component.html',
  styleUrl: './auth-container.component.scss'
})
export class AuthContainerComponent {
  protected currentPage: 'login' | 'signup' = 'login';
}

import { ChangeDetectionStrategy, Component } from '@angular/core';
import { LoginPageComponent } from '../login-page/login-page.component';
import { SignupPageComponent } from '../signup-page/signup-page.component';
import { TuiCardLarge } from '@taiga-ui/layout';

@Component({
  selector: 'app-auth-container',
  imports: [LoginPageComponent, SignupPageComponent, TuiCardLarge],
  templateUrl: './auth-container.component.html',
  styleUrl: './auth-container.component.scss',
})
export class AuthContainerComponent {
  protected currentPage: 'login' | 'signup' = 'login';
}

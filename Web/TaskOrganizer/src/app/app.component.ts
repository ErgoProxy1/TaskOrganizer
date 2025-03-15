import { TuiRoot } from '@taiga-ui/core';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { LoginPageComponent } from './features/auth-feature/login-page/login-page.component';
import { AuthContainerComponent } from './features/auth-feature/auth-container/auth-container.component';

@Component({
  selector: 'app-root',
  imports: [LoginPageComponent, TuiRoot, AuthContainerComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent {
  title = 'task-organizer';
}

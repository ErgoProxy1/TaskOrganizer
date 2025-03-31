import { TuiRoot } from '@taiga-ui/core';
import { ChangeDetectionStrategy, Component, inject, Signal } from '@angular/core';
import { LoginPageComponent } from './features/auth-feature/login-page/login-page.component';
import { AuthContainerComponent } from './features/auth-feature/auth-container/auth-container.component';
import { RouterModule } from '@angular/router';
import { AuthService } from './core/services/auth.service';
import { User } from 'firebase/auth';

@Component({
  selector: 'app-root',
  imports: [TuiRoot, RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'task-organizer';
}

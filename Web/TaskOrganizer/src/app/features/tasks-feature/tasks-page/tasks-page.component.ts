import { ChangeDetectionStrategy, Component, DestroyRef, inject, WritableSignal } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { User } from 'firebase/auth';
import { TuiButton } from '@taiga-ui/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { take } from 'rxjs';

@Component({
  selector: 'app-tasks-page',
  imports: [TuiButton],
  templateUrl: './tasks-page.component.html',
  styleUrl: './tasks-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TasksPageComponent {
  protected userSignal: WritableSignal<User | null>;

  private auth = inject(AuthService);
  private destroyRef = inject(DestroyRef);

  constructor() {
    this.userSignal = this.auth.getUserSignal();
  }

  protected logout() {
    this.auth.logout().subscribe();
  }
}

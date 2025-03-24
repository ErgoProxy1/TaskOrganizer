import { ChangeDetectionStrategy, Component, computed, DestroyRef, inject, WritableSignal } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { User } from 'firebase/auth';
import { TuiButton } from '@taiga-ui/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { take } from 'rxjs';
import { TuiNavigation } from '@taiga-ui/layout';
import { AvatarComponent } from '../../../core/components/avatar/avatar.component';

@Component({
  selector: 'app-projects-page',
  imports: [TuiButton, TuiNavigation, AvatarComponent],
  templateUrl: './projects-page.component.html',
  styleUrl: './projects-page.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProjectsPageComponent {
  protected userSignal: WritableSignal<User | null>;

  private auth = inject(AuthService);
  private destroyRef = inject(DestroyRef);

  constructor() {
    this.userSignal = this.auth.getUserSignal();
  }
}

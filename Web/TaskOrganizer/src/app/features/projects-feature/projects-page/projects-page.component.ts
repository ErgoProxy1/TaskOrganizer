import { ChangeDetectionStrategy, Component, computed, DestroyRef, inject, WritableSignal } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { User } from 'firebase/auth';
import { TuiButton } from '@taiga-ui/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { take } from 'rxjs';
import { TuiNavigation } from '@taiga-ui/layout';
import { AvatarComponent } from '../../../core/components/avatar/avatar.component';
import { ProjectHeaderComponent } from '../project-header/project-header.component';

@Component({
  selector: 'app-projects-page',
  imports: [TuiNavigation, ProjectHeaderComponent],
  templateUrl: './projects-page.component.html',
  styleUrl: './projects-page.component.scss',
})
export class ProjectsPageComponent {
  protected userSignal: WritableSignal<User | null>;

  private auth = inject(AuthService);
  private destroyRef = inject(DestroyRef);

  constructor() {
    this.userSignal = this.auth.getUserSignal();
  }
}

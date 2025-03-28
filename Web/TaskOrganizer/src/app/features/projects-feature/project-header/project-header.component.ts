import { ChangeDetectionStrategy, Component, DestroyRef, inject } from '@angular/core';
import { AvatarComponent } from '../../../core/components/avatar/avatar.component';
import { TuiButton, tuiDialog, TuiDialogService, TuiIcon } from '@taiga-ui/core';
import { NewProjectComponent } from '../new-project/new-project.component';
import { take } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-project-header',
  imports: [AvatarComponent, TuiIcon, TuiButton],
  templateUrl: './project-header.component.html',
  styleUrl: './project-header.component.scss',
})
export class ProjectHeaderComponent {
  private readonly newProjectDialog = tuiDialog(NewProjectComponent, { dismissible: false });

  private dialogRef = inject(TuiDialogService);
  private destroyRef = inject(DestroyRef);
  constructor() {}

  protected openNewProjectDialog(): void {
    this.newProjectDialog()
      .pipe(take(1), takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        console.log('done');
      });
  }
}

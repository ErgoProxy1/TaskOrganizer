import { ChangeDetectionStrategy, Component, DestroyRef, inject } from '@angular/core';
import { AvatarComponent } from '../../../core/components/avatar/avatar.component';
import { TuiAlertService, TuiButton, tuiDialog, TuiDialogService, TuiIcon } from '@taiga-ui/core';
import { NewProjectComponent } from '../new-project/new-project.component';
import { take } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { PolymorpheusComponent } from '@taiga-ui/polymorpheus';

@Component({
  selector: 'app-project-header',
  imports: [AvatarComponent, TuiIcon, TuiButton],
  templateUrl: './project-header.component.html',
  styleUrl: './project-header.component.scss',
})
export class ProjectHeaderComponent {
  private dialogRef = inject(TuiDialogService);
  private destroyRef = inject(DestroyRef);
  private tuiAlert = inject(TuiAlertService);
  constructor() {}

  protected openNewProjectDialog(): void {
    this.dialogRef
      .open<string>(new PolymorpheusComponent(NewProjectComponent), { dismissible: false })
      .pipe(take(1), takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (result) => {
          if (result === 'pass') {
            this.tuiAlert
              .open('Project created successfully', { label: 'Success', appearance: 'success', autoClose: 3000 })
              .pipe(take(1))
              .subscribe();
          } else if (result === 'error') {
            this.tuiAlert
              .open('Error creating project, please verify your connection or try again later', {
                label: 'Error',
                appearance: 'error',
                autoClose: 3000,
              })
              .pipe(take(1))
              .subscribe();
          }
        },
      });
  }
}

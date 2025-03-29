import { HttpClient } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDividerModule } from '@angular/material/divider';
import { TuiButton, TuiError, TuiTextfield, TuiTitle, TuiAppearance, TuiLoader, TuiDialogService, TuiDialogContext } from '@taiga-ui/core';
import { TuiHeader } from '@taiga-ui/layout';
import { TuiInputModule, TuiTextareaModule, TuiTextfieldControllerModule } from '@taiga-ui/legacy';
import { ProjectDTO } from '../../../core/contracts/backend.contracts';
import { AuthService } from '../../../core/services/auth.service';
import { injectContext } from '@taiga-ui/polymorpheus';
import { take } from 'rxjs';

@Component({
  selector: 'app-new-project',
  imports: [
    ReactiveFormsModule,
    TuiButton,
    TuiError,
    TuiTextfield,
    TuiTitle,
    TuiAppearance,
    TuiHeader,
    MatDividerModule,
    TuiLoader,
    TuiTextareaModule,
    TuiInputModule,
    TuiTextfieldControllerModule,
  ],
  templateUrl: './new-project.component.html',
  styleUrl: './new-project.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NewProjectComponent {
  protected submited = signal(false);

  private fb = inject(FormBuilder);
  private http = inject(HttpClient);
  private auth = inject(AuthService);

  protected readonly context = injectContext<TuiDialogContext<string>>();

  protected projectForm = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(60)]],
    description: ['', [Validators.maxLength(400)]],
  });

  protected submit(): void {
    if (this.projectForm.invalid) {
      return;
    }
    this.submited.set(true);
    const name = this.projectForm.controls.name.value ?? '';
    const description = this.projectForm.controls.description.value ?? '';
    const createdByUid = this.auth.getUserSignal()()?.uid ?? '';
    this.http
      .post('/api/projects', { name: name, description: description, createdByUid: createdByUid } as ProjectDTO)
      .pipe(take(1))
      .subscribe({
        next: () => {
          this.context.completeWith('pass');
        },
        error: () => {
          this.context.completeWith('error');
        },
      });
  }
}

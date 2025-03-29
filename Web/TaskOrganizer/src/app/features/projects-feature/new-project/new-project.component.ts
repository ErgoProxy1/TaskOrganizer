import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDividerModule } from '@angular/material/divider';
import { TuiButton, TuiError, TuiTextfield, TuiTitle, TuiAppearance, TuiLoader } from '@taiga-ui/core';
import { TuiHeader } from '@taiga-ui/layout';
import { TuiInputModule, TuiTextareaModule, TuiTextfieldControllerModule } from '@taiga-ui/legacy';

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
  private fb = inject(FormBuilder);
  protected projectForm = this.fb.group({
    title: ['', [Validators.required, Validators.maxLength(60)]],
    description: ['', [Validators.maxLength(400)]],
  });
}

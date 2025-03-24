import { ChangeDetectionStrategy, Component } from '@angular/core';
import { AvatarComponent } from '../../../core/components/avatar/avatar.component';
import { TuiButton, TuiIcon } from '@taiga-ui/core';

@Component({
  selector: 'app-project-header',
  imports: [AvatarComponent, TuiIcon, TuiButton],
  templateUrl: './project-header.component.html',
  styleUrl: './project-header.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProjectHeaderComponent {}

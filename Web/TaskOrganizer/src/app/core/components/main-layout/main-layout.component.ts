import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TuiNavigation } from '@taiga-ui/layout';

@Component({
  selector: 'app-main-layout',
  imports: [TuiNavigation, RouterOutlet],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MainLayoutComponent {
  expanded = signal(false);
}

import { TuiRoot } from "@taiga-ui/core";
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoginPageComponent } from "./features/login-page/login-page.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, LoginPageComponent, TuiRoot],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent {
  title = 'task-organizer';
}

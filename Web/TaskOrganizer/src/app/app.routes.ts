import { Routes } from '@angular/router';
import { AuthContainerComponent } from './features/auth-feature/auth-container/auth-container.component';
import { TasksPageComponent } from './features/tasks-feature/tasks-page/tasks-page.component';
import { noAuthGuard } from './core/guards/auth-guard.guard';
import { isAuthGuard } from './core/guards/is-auth.guard';

export const routes: Routes = [
  { component: AuthContainerComponent, path: '', canActivate: [isAuthGuard] },
  { component: TasksPageComponent, path: 'tasks', canActivate: [noAuthGuard] },
];

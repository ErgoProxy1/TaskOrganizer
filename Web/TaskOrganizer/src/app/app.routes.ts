import { Routes } from '@angular/router';
import { AuthContainerComponent } from './features/auth-feature/auth-container/auth-container.component';
import { TasksPageComponent } from './features/tasks-feature/tasks-page/tasks-page.component';
import { authGuard } from './core/guards/auth-guard.guard';

export const routes: Routes = [
  { component: AuthContainerComponent, path: '' },
  { component: TasksPageComponent, path: 'tasks', canActivate: [authGuard] },
];

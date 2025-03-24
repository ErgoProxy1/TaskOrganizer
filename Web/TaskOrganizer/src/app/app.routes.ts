import { Routes } from '@angular/router';
import { AuthContainerComponent } from './features/auth-feature/auth-container/auth-container.component';
import { ProjectsPageComponent } from './features/projects-feature/projects-page/projects-page.component';
import { noAuthGuard } from './core/guards/auth-guard.guard';
import { isAuthGuard } from './core/guards/is-auth.guard';
import { MainLayoutComponent } from './core/components/main-layout/main-layout.component';

export const routes: Routes = [
  { component: AuthContainerComponent, path: '', canActivate: [isAuthGuard] },
  { component: MainLayoutComponent, path: 'app', canActivate: [noAuthGuard], children: [{ component: ProjectsPageComponent, path: 'projects' }] },
];

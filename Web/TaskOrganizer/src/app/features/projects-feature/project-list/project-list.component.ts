import { AfterViewInit, ChangeDetectionStrategy, Component, inject, OnInit, resource, ResourceRef, Signal, WritableSignal } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { User } from 'firebase/auth';
import { HttpClient } from '@angular/common/http';
import { rxResource, takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ProjectModel } from '../../../core/models/api.models';
import { TuiButton, TuiLoader } from '@taiga-ui/core';
import { Observable, tap } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-project-list',
  imports: [TuiLoader, AsyncPipe],
  templateUrl: './project-list.component.html',
  styleUrl: './project-list.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProjectListComponent {
  protected projects$: Observable<ProjectModel[]>;
  protected userSignal: WritableSignal<User | null>;

  private auth = inject(AuthService);
  private http = inject(HttpClient);

  constructor() {
    this.userSignal = this.auth.getUserSignal();
    this.projects$ = this.http.get<ProjectModel[]>(`/api/projects?userId=${this.userSignal()?.uid}`).pipe(
      tap(() => console.log(this.userSignal())),
      takeUntilDestroyed(),
    );
  }
}

import { ChangeDetectionStrategy, Component, computed, DestroyRef, inject, signal, WritableSignal } from '@angular/core';
import { TUI_CONFIRM, TuiAvatar, TuiConfirmData } from '@taiga-ui/kit';
import { AuthService } from '../../services/auth.service';
import { User } from 'firebase/auth';
import { TuiObscured } from '@taiga-ui/cdk/directives/obscured';
import { TuiActiveZone } from '@taiga-ui/cdk/directives/active-zone';
import { TuiButton, TuiDialogService, TuiDropdown } from '@taiga-ui/core';
import { switchMap, take } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-avatar',
  imports: [TuiAvatar, TuiDropdown, TuiObscured, TuiActiveZone, TuiButton],
  templateUrl: './avatar.component.html',
  styleUrl: './avatar.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AvatarComponent {
  protected userSignal: WritableSignal<User | null>;
  protected userInitials = computed(() => {
    return (
      this.userSignal()
        ?.displayName?.split(' ')
        .map((name) => name[0].toUpperCase()[0])
        .join('') ?? '@tui.user'
    );
  });

  protected dropdownOpen = signal(false);

  private auth = inject(AuthService);
  private readonly dialogs = inject(TuiDialogService);
  private readonly destroyRef = inject(DestroyRef);

  constructor() {
    this.userSignal = this.auth.getUserSignal();
  }

  protected onClick(): void {
    this.dropdownOpen.update((prev) => !prev);
  }

  protected onObscured(obscured: boolean): void {
    if (obscured) {
      this.dropdownOpen.set(false);
    }
  }

  protected onActiveZone(active: boolean): void {
    this.dropdownOpen.update((prev) => active && prev);
  }

  protected logout() {
    const data: TuiConfirmData = {
      content: 'Do you really want to logout?',
      yes: 'Confirm',
      no: 'Cancel',
    };

    this.dialogs
      .open<boolean>(TUI_CONFIRM, {
        label: 'Logout',
        size: 's',
        data,
      })
      .pipe(
        switchMap((isConfirmed) => (isConfirmed ? this.auth.logout() : '')),
        take(1),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }

  protected navigateToProfile() {
    throw new Error('Not implemented');
  }

  protected navigateToSettings() {
    throw new Error('Not implemented');
  }
}

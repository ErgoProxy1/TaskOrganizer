export class AuthError extends Error {
  icon = '';

  constructor(message: string, icon: string) {
    super(message);
    this.icon = icon;
  }
}

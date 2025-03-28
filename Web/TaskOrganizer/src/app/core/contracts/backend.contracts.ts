export class VerifyTokenRequest {
  public IdToken: string = '';
}

export class SignupRequest {
  public email = '';
  public password = '';
  public username = '';
}

export class ProjectDTO {
  public name = '';
  public description = '';
  public createdByUid = '';
}

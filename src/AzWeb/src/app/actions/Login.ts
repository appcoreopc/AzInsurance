import { Action } from '@ngrx/store';

export enum ActionTypes {

  Login = 'Login',

  Logout = 'Logout',

  Reset = 'Reset',
}

export class Login implements Action {
  readonly type = ActionTypes.Login;
}

export class Logout implements Action {
  readonly type = ActionTypes.Logout;
}

export class Reset implements Action {
  readonly type = ActionTypes.Reset;
}

export type ActionsUnion = Login | Logout | Reset;
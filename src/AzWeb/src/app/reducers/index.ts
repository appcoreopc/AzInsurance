import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store';
import { environment } from '../../environments/environment';
import { ActionsUnion, ActionTypes, Login } from '../actions/Login';
import { Action } from '@ngrx/store';
import { debug } from 'util';

export interface State {
   type : string, 
   actionValue : number;
}

export const initialState: State = {
  type: "empty",
  actionValue : 0,
};

// export const reducers: ActionReducerMap<State> = {    
// };
// export const metaReducers: MetaReducer<State>[] = !environment.production ? [] : [];

export function LoginReducer(state = initialState, action: Login): State {

  debug;
  console.log("store reducer happening...")
  switch (action.type) {
    case ActionTypes.Login:
    console.log("login reducer");
    return {
       ...state,
      type : state.type,
      actionValue : state.actionValue,
    };
   defaut: 
    return initialState;
  }
}
import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { ActionTypes } from './actions/Login';
import { map, mergeMap } from 'rxjs/operators';

@Injectable()
export class AppEffects {

  constructor(private actions$: Actions) {

  }

  @Effect() 
  loginEffects$ = this.actions$.pipe(ofType(ActionTypes.Login),
   map(action => {
    console.log("login effect taking place....")     
    return ({ type: "teste" });
  }));

}

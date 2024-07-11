import {Actions, createEffect, ofType} from "@ngrx/effects";
import * as authActions from "./auth.actions";
import {Injectable} from "@angular/core";
import {AuthService} from "./auth.service";
import {catchError, map, mergeMap, of} from "rxjs";


@Injectable()
export class AuthEffects {

  $login = createEffect(() =>
  this.actions$.pipe(
    ofType(authActions.LoginUser),
    mergeMap((action) => this.authService.login(action.loginRequest).pipe(
      map(loginResponse =>
         authActions.LoginUserSuccess({loginResponse})
      ),
      catchError(error => {
        console.log(error);
        return of(authActions.LoginUserFailure(error))
      })
    ))
  ))

  $register = createEffect(() =>
    this.actions$.pipe(
      ofType(authActions.RegisterUser),
      mergeMap((action) => this.authService.register(action.registerRequest).pipe(
        map(registerResponse =>
          authActions.RegisterUserSuccess({registerResponse})
        ),
        catchError(error => of(authActions.RegisterUserFailure(error)))
      ))
    ))

  constructor(
    private actions$: Actions,
    private authService: AuthService) {
  }
}

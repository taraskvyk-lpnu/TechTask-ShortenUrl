import { createReducer, on} from "@ngrx/store";
import * as authActions from "./auth.actions";
import {User} from "../models/user";
import {AuthRequest} from "../models/authRequest";
import {AuthenticationResponse} from "../models/authenticationResponse";

export const initialState : User = {
  id: 0,
  roles: [],
  jwtToken: ''
};


export const AuthReducer = createReducer(
  initialState,
  on(authActions.LoginUser, (state, {loginRequest: AuthRequest}) => {
    console.log(state)
    return state
  }),
  on(authActions.LoginUserSuccess, (state, {loginResponse}) => {
    const newState = {
      ...state,
      id: loginResponse.id,
      roles: loginResponse.roles,
      jwtToken: loginResponse.jwtToken
    };

    return newState;
  }),
  on(authActions.LoginUserFailure, (state, {error}) => {
    console.log(error);
    return state;
  }),

  on(authActions.RegisterUser, (state, {registerRequest: AuthRequest}) => state),
  on(authActions.RegisterUserSuccess, (state, {registerResponse}) => {
    const newState = {
      ...state,
      id: registerResponse.id,
      roles: registerResponse.roles,
      jwtToken: registerResponse.jwtToken
    };

    return newState;
  }),
  on(authActions.RegisterUserFailure, (state, {error}) => {
    console.log(error);
    return state;
  }),
);

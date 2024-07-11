import { createAction, props } from "@ngrx/store";
import {AuthRequest} from "../models/authRequest";
import {AuthenticationResponse} from "../models/authenticationResponse";


export const RegisterUser = createAction("[Register] Register User", props<{ registerRequest: AuthRequest }>());
export const RegisterUserSuccess = createAction("[Register] Register User Success", props<{ registerResponse: AuthenticationResponse }>());
export const RegisterUserFailure = createAction("[Register] Register User Failure", props<{ error: any }>());

export const LoginUser = createAction("[Login] Login User", props<{ loginRequest: AuthRequest }>());
export const LoginUserSuccess = createAction("[Login] Login User Success", props<{ loginResponse: AuthenticationResponse }>());
export const LoginUserFailure = createAction("[Login] Login User Failure", props<{ error: any }>());

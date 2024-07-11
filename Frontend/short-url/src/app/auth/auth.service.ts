import { Injectable } from '@angular/core';
import {AuthRequest} from "../models/authRequest";
import { environment } from "../environments/environment.development";
import {HttpClient} from "@angular/common/http";
import {AuthenticationResponse} from "../models/authenticationResponse";
import {log} from "@angular-devkit/build-angular/src/builders/ssr-dev-server";

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  authApi= environment.apiUrl + '/api/account';

  private user = { id: 1, isAdmin: false };

  constructor(private httpClient : HttpClient) {
  }

  getUserId(): number {
    return this.user.id;
  }

  register(registerRequest: AuthRequest) {
    return this.httpClient.post<AuthenticationResponse>(this.authApi + '/register', registerRequest);
  }

  login(loginRequest: AuthRequest) {
    return this.httpClient.post<AuthenticationResponse>(this.authApi + '/login', loginRequest);
  }

  isAuthorized(): boolean {
    return !!this.user;
  }

  isAdmin(): boolean {
    return this.user.isAdmin;
  }
}

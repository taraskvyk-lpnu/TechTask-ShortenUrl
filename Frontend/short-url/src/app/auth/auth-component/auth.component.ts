import { Component } from '@angular/core';
import {AuthRequest} from "../../models/authRequest";
import {AuthService} from "../auth.service";
import {Store} from "@ngrx/store";
import {AppState} from "../../app.state";
import {FormsModule} from "@angular/forms";
import {LoginUser, RegisterUser} from "../auth.actions";
import {MatFormField} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {NgIf} from "@angular/common";
import {Router} from "@angular/router";

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [
    FormsModule,
    MatFormField,
    MatInput,
    NgIf
  ],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css'
})
export class AuthComponent {
  authRequest: AuthRequest = {
    email: '',
    password: ''
  }

  constructor(private store: Store<AppState>, private router: Router) {
  }

  logIn(){
    console.log(this.authRequest)
    this.store.dispatch(LoginUser({loginRequest: this.authRequest}));
    this.resetForm();
    this.router.navigate(['']);
  }

  register(){
    this.store.dispatch(RegisterUser({registerRequest: this.authRequest}));
    this.resetForm();
    this.router.navigate(['']);
  }

  resetForm(){
    this.authRequest = {
      email: '',
      password: ''
    }
  }
}

import {Component, OnInit} from '@angular/core';
import {MatToolbar} from "@angular/material/toolbar";
import {MatButton} from "@angular/material/button";
import {RouterLink} from "@angular/router";
import {Observable} from "rxjs";
import {User} from "../models/user";
import {select, Store} from "@ngrx/store";
import {AppState} from "../app.state";
import {AuthService} from "../auth/auth.service";
import {AsyncPipe, NgIf} from "@angular/common";

@Component({
  selector: 'app-navigation',
  standalone: true,
  imports: [
    MatToolbar,
    MatButton,
    RouterLink,
    AsyncPipe,
    NgIf
  ],
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.css'
})
export class NavigationComponent implements OnInit {

  user$: Observable<User>;

  constructor(private store: Store<AppState>, private authService: AuthService) {
    this.user$ = store.pipe(select('user'));
  }

  ngOnInit(): void {
    this.user$.subscribe((user) => {
      if (user) {
        console.log('User loaded:', user);
      }
    });
  }
}

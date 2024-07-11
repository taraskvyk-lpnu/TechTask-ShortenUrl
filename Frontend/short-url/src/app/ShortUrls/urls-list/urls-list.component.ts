import {Component, OnInit} from '@angular/core';
import {async, Observable, Subscription} from "rxjs";
import {ShortUrl} from "../../models/shortUrl";
import {AppState} from "../../app.state";
import {select, Store} from "@ngrx/store";
import {AddShortUrl, DeleteShortUrl, GetAllShortUrls, GetShortUrl, UpdateShortUrl} from "../short-urls.actions";
import {AsyncPipe} from "@angular/common";
import {AuthService} from "../../auth/auth.service";
import { CommonModule } from "@angular/common";
import {RouterLink} from "@angular/router";
import {User} from "../../models/user";

@Component({
  selector: 'app-urls-list',
  standalone: true,
  imports: [
    AsyncPipe,
    CommonModule,
    RouterLink
  ],
  templateUrl: './urls-list.component.html',
  styleUrl: './urls-list.component.css'
})
export class UrlsListComponent implements OnInit {
  shortUrls$: Observable<ShortUrl[]> = new Observable();
  user$: Observable<User>;
  user: User = {
    id: 0,
    roles: [],
    jwtToken: ''
  };
  private userSubscription: Subscription;

  constructor(private store: Store<AppState>, private authService: AuthService) {
    this.shortUrls$ = store.pipe(select('shortUrls'));
    this.user$ = store.pipe(select('user'));
    this.userSubscription = store.pipe(select('user')).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.getAllShortUrls();
    this.user$.subscribe((user) => {
      if (user) {
        console.log('User loaded:', user);
      }
    });
  }

  canDelete(url: ShortUrl): boolean {
    return this.user.roles.includes('Admin') || (url.createdByUserId === this.user.id);
  }

  getAllShortUrls() {
    this.store.dispatch(GetAllShortUrls());
  }

  getShortUrlById(id: number) {
    this.store.dispatch(GetShortUrl({shortUrlId: id}));
  }

  addShortUrl(shortUrl: ShortUrl) {
    this.store.dispatch(AddShortUrl({ shortUrl: shortUrl }));
  }

  updateShortUrl(id: number, shortUrl: ShortUrl) {
    this.store.dispatch(UpdateShortUrl({ shortUrl: shortUrl }));
  }

  deleteShortUrlById(shortUrlId: number) {
    console.log(shortUrlId, this.user.id)
    this.store.dispatch(DeleteShortUrl({shortUrlId, userId: this.user.id}));
  }
}

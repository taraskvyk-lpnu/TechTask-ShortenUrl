import { Component } from '@angular/core';
import {ShortUrl} from "../models/shortUrl";
import {select, Store} from "@ngrx/store";
import {AppState} from "../app.state";
import {AuthService} from "../auth/auth.service";
import {ShortUrlService} from "../ShortUrls/short-url.service";
import {AddShortUrl} from "../ShortUrls/short-urls.actions";
import {FormsModule} from "@angular/forms";
import {AddShortUrlRequest} from "../models/addShortUrl";
import {ActivatedRoute, Router} from "@angular/router";
import {User} from "../models/user";
import {Subscription} from "rxjs";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-add-url-form',
  standalone: true,
  imports: [
    FormsModule,
    NgIf
  ],
  templateUrl: './add-url-form.component.html',
  styleUrl: './add-url-form.component.css'
})
export class AddUrlFormComponent {
  shortUrl: AddShortUrlRequest = {
    description: '',
    createdByUserId: 0,
    originalUrl: ''
  };

  user: User = {
    id: 0,
    roles: [],
    jwtToken: ''
  };
  private userSubscription: Subscription;

  constructor(private store: Store<AppState>, private activatedRoute: ActivatedRoute, private router: Router) {
    this.userSubscription = store.pipe(select('user')).subscribe(user => this.user = user);
  }

  addShortUrl() {
    this.shortUrl.createdByUserId = this.user.id;
    this.store.dispatch(AddShortUrl({ shortUrl: this.shortUrl }));
    this.resetForm();
    this.router.navigate(['/urls-table']);
  }

  resetForm(){
    this.shortUrl = {
      description: '',
      createdByUserId: 0,
      originalUrl: ''
    };
  }
}

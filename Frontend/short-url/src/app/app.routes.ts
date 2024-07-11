import { Routes } from '@angular/router';
import {AppComponent} from "./app.component";
import {UrlsListComponent} from "./ShortUrls/urls-list/urls-list.component";
import {AddUrlFormComponent} from "./add-url-form/add-url-form.component";
import {AuthComponent} from "./auth/auth-component/auth.component";

export const routes: Routes = [
  { path: '', redirectTo: 'urls-table', pathMatch: 'full' },
  { path: 'urls-table', component: UrlsListComponent },
  { path: 'add-url', component: AddUrlFormComponent },
  { path: 'auth', component: AuthComponent }
];

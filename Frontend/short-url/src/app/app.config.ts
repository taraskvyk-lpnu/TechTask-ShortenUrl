import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {provideHttpClient} from "@angular/common/http";
import {provideStore} from "@ngrx/store";
import {AppState} from "./app.state";
import {ShortUrlsReducer} from "./ShortUrls/short-urls.reducers";
import {provideEffects} from "@ngrx/effects";
import {ShortUrlsEffects} from "./ShortUrls/short-urls.effects";
import {AuthReducer} from "./auth/auth.reducers";
import {AuthEffects} from "./auth/auth.effects";

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    provideStore<AppState>({shortUrls : ShortUrlsReducer, user: AuthReducer }),
    provideEffects([ShortUrlsEffects, AuthEffects])
  ]
};

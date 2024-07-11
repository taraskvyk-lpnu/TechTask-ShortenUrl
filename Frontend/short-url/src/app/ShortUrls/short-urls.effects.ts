import {Actions, createEffect, ofType} from "@ngrx/effects";
import * as urlActions from '../ShortUrls/short-urls.actions';
import {mergeMap, map, catchError, of} from "rxjs";
import {Injectable} from "@angular/core";
import {ShortUrlService} from "./short-url.service";


@Injectable()
export class ShortUrlsEffects {

  addShortUrl$ = createEffect(() =>
  this.actions$.pipe(
    ofType(urlActions.AddShortUrl),
    mergeMap((action) =>
    this.shortUrlService.addShortUrl(action.shortUrl).pipe(
      map(shortUrl => urlActions.AddShortUrlSuccess({ shortUrl })),
      catchError(error => of(urlActions.AddShortUrlFailure(error)))
    ))
  ))

  updateShortUrl$ = createEffect(() =>
    this.actions$.pipe(
      ofType(urlActions.UpdateShortUrl),
      mergeMap((action) =>
        this.shortUrlService.updateShortUrl(action.shortUrl).pipe(
          map(shortUrl => urlActions.UpdateShortUrlSuccess({ shortUrl })),
          catchError(error => of(urlActions.UpdateShortUrlFailure(error)))
        ))
    ))

  deleteShortUrl$ = createEffect(() =>
    this.actions$.pipe(
      ofType(urlActions.DeleteShortUrl),
      mergeMap((action) =>
        this.shortUrlService.removeShortUrlById(action.shortUrlId, action.userId).pipe(
          map((shortUrlId, userId) =>
            urlActions.DeleteShortUrlSuccess({ shortUrlId })),
          catchError(error => of(urlActions.DeleteShortUrlFailure(error)))
        ))
    ))

  getAllShortUrls$ = createEffect(() =>
    this.actions$.pipe(
      ofType(urlActions.GetAllShortUrls),
      mergeMap((action) =>
        this.shortUrlService.getAll().pipe(
          map(shortUrls => urlActions.GetAllShortUrlsSuccess({ shortUrls })),
          catchError(error => of(urlActions.GetAllShortUrlsFailure(error)))
        ))
    ))

  getShortUrl$ = createEffect(() =>
    this.actions$.pipe(
      ofType(urlActions.GetShortUrl),
      mergeMap((action) =>
        this.shortUrlService.getShortUrlById(action.shortUrlId).pipe(
          map(shortUrl => urlActions.GetShortUrlSuccess({ shortUrl })),
          catchError(error => of(urlActions.GetShortUrlFailure(error)))
        ))
    ))

  constructor(
    private actions$: Actions,
    private shortUrlService: ShortUrlService) {}
}

import { createReducer, on} from "@ngrx/store";
import {ShortUrl} from "../models/shortUrl";
import * as urlsActions from "./short-urls.actions";

export const initialState : ShortUrl[] = [];

export const ShortUrlsReducer = createReducer(
  initialState,
  on(urlsActions.AddShortUrl, (state, shortUrl) => state),
  on(urlsActions.AddShortUrlSuccess, (state, {shortUrl}) =>
  {
    console.log('Reducer - ShortUrl Added:', shortUrl);
    return [...state, shortUrl]
  }),
  on(urlsActions.AddShortUrlFailure, (state, {error}) => {
    console.log(error)
    return state;
  }),

  on(urlsActions.UpdateShortUrl, (state, { shortUrl }) => state),
  on(urlsActions.UpdateShortUrlSuccess, (state, { shortUrl}) => {
    state = state.filter(shortUrl => shortUrl.id !== shortUrl.id);
    return [...state, shortUrl]
  }),
  on(urlsActions.UpdateShortUrlFailure, (state, {error}) => {
    console.log(error)
    return state;
  }),

  on(urlsActions.DeleteShortUrl, (state, {shortUrlId, userId}) => state),
  on(urlsActions.DeleteShortUrlSuccess, (state, {shortUrlId}) => {

    console.log('Reducer - ShortUrl Removed:', shortUrlId);
    return state.filter(Url => Url.id !== shortUrlId);
  }),
  on(urlsActions.DeleteShortUrlFailure, (state, {error}) => {
    console.log(error);
    return state;
  }),

  on(urlsActions.GetAllShortUrls, (state) => {
    console.log('Reducer - Loading Urls...');
    return state;
  }),
  on(urlsActions.GetAllShortUrlsSuccess, (state, { shortUrls }) => {
    state = shortUrls;
    console.log('Reducer - Urls Loaded:', shortUrls);
    return state;
  }),
  on(urlsActions.GetAllShortUrlsFailure, (state, { error }) => {
    console.error('Error getting urls:', error);
    return state;
  }),

  on(urlsActions.GetShortUrl, (state) => {
    console.log('Reducer - Loading Url...');
    return state;
  }),
  on(urlsActions.GetShortUrlSuccess, (state, { shortUrl }) => {
    console.log('Reducer - Url Loaded:', shortUrl);
    return state;
  }),
  on(urlsActions.GetShortUrlFailure, (state, { error }) => {
    console.error('Error getting urls:', error);
    return state;
  }),
)

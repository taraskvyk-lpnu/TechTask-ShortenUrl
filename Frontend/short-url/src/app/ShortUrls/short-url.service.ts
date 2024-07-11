import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { environment } from "../environments/environment.development";
import { HttpClient } from "@angular/common/http";
import {ShortUrl} from "../models/shortUrl";
import {AddShortUrlRequest} from "../models/addShortUrl";

@Injectable({
  providedIn: 'root'
})
export class ShortUrlService {

  apiUrl = environment.apiUrl + '/api/ShortUrls';

  constructor(private httpClient : HttpClient) { }

  getAll(){
    return this.httpClient.get<ShortUrl[]>(this.apiUrl);
  }

  getShortUrlById(id: number): Observable<ShortUrl> {
    return this.httpClient.get<ShortUrl>(this.apiUrl + `/${id}`);
  }

  addShortUrl(addShortUrlRequest: AddShortUrlRequest): Observable<ShortUrl> {
    return this.httpClient.post<ShortUrl>(this.apiUrl, addShortUrlRequest);
  }

  updateShortUrl(shortUrl: ShortUrl): Observable<ShortUrl> {
    return this.httpClient.put<ShortUrl>(this.apiUrl, shortUrl);
  }

  removeShortUrlById(id: number, userId: number): Observable<number> {
    console.log('Service removes: ', id, userId)
    return this.httpClient.delete<number>(this.apiUrl + `/${id}`, {params: {userId : userId}});
  }
}

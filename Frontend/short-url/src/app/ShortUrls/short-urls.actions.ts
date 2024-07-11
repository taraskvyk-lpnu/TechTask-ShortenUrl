import { createAction, props } from "@ngrx/store";
import { ShortUrl } from "../models/shortUrl";
import {AddShortUrlRequest} from "../models/addShortUrl";

export const GetAllShortUrls = createAction('[ShortUrl] Get All ShortUrls');
export const GetAllShortUrlsSuccess = createAction('[ShortUrl] Get All ShortUrls Success', props<{ shortUrls: ShortUrl[] }>());
export const GetAllShortUrlsFailure = createAction('[ShortUrl] Get All ShortUrls Failure', props<{ error: any }>());

export const GetShortUrl = createAction('[ShortUrl] Get ShortUrl', props<{ shortUrlId: number }>());
export const GetShortUrlSuccess = createAction('[ShortUrl] Get ShortUrl Success', props<{ shortUrl: ShortUrl }>());
export const GetShortUrlFailure = createAction('[ShortUrl] Get ShortUrl Failure', props<{ error: any }>());

export const AddShortUrl = createAction("[ShortUrl] Add ShortUrl", props<{ shortUrl: AddShortUrlRequest }>());
export const AddShortUrlSuccess = createAction("[ShortUrl] Add ShortUrl Success", props<{ shortUrl: ShortUrl }>());
export const AddShortUrlFailure = createAction("[ShortUrl] Add Short Url Failure", props<{ error: any }>());

export const UpdateShortUrl = createAction('[ShortUrl] Update ShortUrl', props<{ shortUrl: ShortUrl }>());
export const UpdateShortUrlSuccess = createAction('[ShortUrl] Update ShortUrl Success', props<{ shortUrl: ShortUrl }>());
export const UpdateShortUrlFailure = createAction('[ShortUrl] Update ShortUrl Failure', props<{error: any}>());

export const DeleteShortUrl = createAction('[ShortUrl] Delete ShortUrl', props<{ shortUrlId: number, userId: number }>());
export const DeleteShortUrlSuccess = createAction('[ShortUrl] Delete ShortUrl Success', props<{ shortUrlId: number }>());
export const DeleteShortUrlFailure = createAction('[ShortUrl] Delete ShortUrl Failure', props<{error: any}>());

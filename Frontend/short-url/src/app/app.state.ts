import {ShortUrl} from "./models/shortUrl";
import {User} from "./models/user";

export class AppState {
  shortUrls: ShortUrl[] = [];
  user: User = {
    id: 0,
    roles: [],
    jwtToken: ''
  };
}

export interface ShortUrl {
  id: number,
  description: string,
  originalUrl: string,
  shortenUrl: string,
  createdAt: Date,
  createdByUserId: number
}

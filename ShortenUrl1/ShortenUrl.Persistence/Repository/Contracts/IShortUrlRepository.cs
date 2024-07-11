using ShortenUrl.Domain.Entities;

namespace ShortenUrl.Persistence.Repository.Contracts;

public interface IShortUrlRepository : IRepository<ShortUrl>
{
    public Task<ShortUrl?> GetShortUrlByOriginUrlAsync(string originalUrl);
    public Task<bool> UpdateAsync(ShortUrl entity);
}
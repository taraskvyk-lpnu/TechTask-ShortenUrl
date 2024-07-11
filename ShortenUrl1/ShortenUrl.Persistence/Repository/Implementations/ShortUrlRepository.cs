using Microsoft.EntityFrameworkCore;
using ShortenUrl.Domain.Entities;
using ShortenUrl.Persistence.Repository.Contracts;

namespace ShortenUrl.Persistence.Repository.Implementations;

public class ShortUrlRepository : Repository<ShortUrl>, IRepository<ShortUrl>, IShortUrlRepository
{
    public ShortUrlRepository(IApplicationDbContext context) : base(context)
    {
    }

    public async Task<ShortUrl?> GetShortUrlByOriginUrlAsync(string originalUrl)
    {
        var shortUrl = await _dbSet.FirstOrDefaultAsync(u => u.OriginalUrl == originalUrl);
        return shortUrl;
    }

    public async Task<bool> UpdateAsync(ShortUrl entity)
    {
        var entityToUpdate = await _dbSet.FindAsync(entity.Id);
        if (entityToUpdate == null)
            return false;
        
        entityToUpdate.CreatedByUserId = entity.CreatedByUserId;
        entityToUpdate.Description = entity.Description;
        
        await SaveChangesAsync();
        return true;
    }
}
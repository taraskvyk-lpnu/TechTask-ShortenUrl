using Microsoft.EntityFrameworkCore;
using ShortenUrl.Domain.Auth;
using ShortenUrl.Domain.Entities;

namespace ShortenUrl.Persistence;

public interface IApplicationDbContext
{
    DbSet<ShortUrl> ShortUrls { get; set; }
    //DbSet<ApplicationUser> Users { get; set; }
    DbSet<T> Set<T>() where T : class;
    Task<int> SaveChangesAsync();
}
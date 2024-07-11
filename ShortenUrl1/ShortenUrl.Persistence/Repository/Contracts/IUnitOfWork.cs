using Microsoft.EntityFrameworkCore;
using ShortenUrl.Domain.Auth;
using ShortenUrl.Domain.Entities;

namespace ShortenUrl.Persistence.Repository.Contracts;

public interface IUnitOfWork
{
    IShortUrlRepository ShortUrlsRepository { get; }
    Task<int> SaveChangesAsync();
}
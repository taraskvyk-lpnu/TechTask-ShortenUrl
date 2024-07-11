using Microsoft.EntityFrameworkCore;
using ShortenUrl.Domain.Auth;
using ShortenUrl.Domain.Entities;
using ShortenUrl.Persistence.Repository.Contracts;

namespace ShortenUrl.Persistence.Repository.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IShortUrlRepository ShortUrlsRepository { get; }

    public UnitOfWork(
        ApplicationDbContext context, 
        IShortUrlRepository shortUrlRepository)
    {
        _context = context;
        ShortUrlsRepository = shortUrlRepository;
    }
    
    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
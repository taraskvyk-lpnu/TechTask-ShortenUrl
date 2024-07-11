using Microsoft.EntityFrameworkCore;
using ShortenUrl.Domain.Entities;
using ShortenUrl.Persistence;
using ShortenUrl.Persistence.Repository.Implementations;

namespace ShortenUrl.Tests.xUnit;

public class ShortUrlRepositoryTests
{
    private readonly DbContextOptions<ApplicationDbContext> _options;

    public ShortUrlRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "test_database")
            .Options;
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        using (var context = new ApplicationDbContext(_options))
        {
            context.ShortUrls.RemoveRange(context.ShortUrls);
            await context.SaveChangesAsync();
            
            var repository = new Repository<ShortUrl>(context);
            var expectedEntities = new List<ShortUrl>
            {
                new ShortUrl
                {
                    OriginalUrl = "https://www.google.com/23456ytgfderf",
                    ShortenUrl = "https://www.google.com/abc",
                    CreatedByUserId = "1",
                    Description = "Google",
                    CreatedDate = DateTime.Now
                }
            };
            await context.ShortUrls.AddRangeAsync(expectedEntities);
            await context.SaveChangesAsync();
            
            // Act
            var actualEntities = await repository.GetAllAsync();

            // Assert
            Assert.Equal(expectedEntities.Count, actualEntities.Count());
        }
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnEntityWithMatchingId()
    {
        // Arrange
        using (var context = new ApplicationDbContext(_options))
        {
            var repository = new Repository<ShortUrl>(context);
            var expectedEntity = new ShortUrl
            {
                OriginalUrl = "https://www.google.com/23456ytgfderf",
                ShortenUrl = "https://www.google.com/abc",
                CreatedByUserId = "1",
                Description = "Google",
                CreatedDate = DateTime.Now
            };
            await context.ShortUrls.AddAsync(expectedEntity);
            await context.SaveChangesAsync();
            
            // Act
            var actualEntity = await repository.GetByIdAsync(1);

            // Assert
            Assert.Equal(expectedEntity.OriginalUrl, actualEntity.OriginalUrl);
        }
    }

    [Fact]
    public async Task AddAsync_ShouldAddNewEntity()
    {
        // Arrange
        using (var context = new ApplicationDbContext(_options))
        {
            var repository = new Repository<ShortUrl>(context);
            var entityToAdd = new ShortUrl
            {
                OriginalUrl = "https://www.google.com/23456ytgfderf",
                ShortenUrl = "https://www.google.com/abc",
                CreatedByUserId = "1",
                Description = "Google",
                CreatedDate = DateTime.Now
            };

            // Act
            var result = await repository.AddAsync(entityToAdd);

            // Assert
            Assert.True(result);

            var addedEntity = await context.ShortUrls.FirstOrDefaultAsync(u => u.OriginalUrl == "https://www.google.com/23456ytgfderf");
            Assert.NotNull(addedEntity);
        }
    }

    [Fact]
    public async Task RemoveByIdAsync_ShouldRemoveEntityWithMatchingId()
    {
        // Arrange
        using (var context = new ApplicationDbContext(_options))
        {
            var repository = new Repository<ShortUrl>(context);
            var entityToRemove = new ShortUrl
            {
                OriginalUrl = "https://www.google.com/23456ytgfderf",
                ShortenUrl = "https://www.google.com/abc",
                CreatedByUserId = "1",
                Description = "Google",
                CreatedDate = DateTime.Now
            };
            
            await context.ShortUrls.AddAsync(entityToRemove);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.RemoveByIdAsync(1);

            // Assert
            Assert.True(result);

            var removedEntity = await context.ShortUrls.FindAsync(1);
            Assert.Null(removedEntity);
        }
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnNullForNonExistingId()
    {
        // Arrange
        using (var context = new ApplicationDbContext(_options))
        {
            var repository = new Repository<ShortUrl>(context);

            // Act
            var actualEntity = await repository.GetByIdAsync(999); 

            // Assert
            Assert.Null(actualEntity);
        }
    }
    
    [Fact]
    public async Task RemoveByIdAsync_ShouldReturnFalseForNonExistingId()
    {
        // Arrange
        using (var context = new ApplicationDbContext(_options))
        {
            var repository = new Repository<ShortUrl>(context);

            // Act
            var result = await repository.RemoveByIdAsync(999); 

            // Assert
            Assert.False(result);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using ShortenUrl.Domain.Entities;
using ShortenUrl.Persistence;

namespace ShortenUrl.Tests.xUnit;

public class ApplicationDbContextTests
{
    [Fact]
    public void CanInsertShortUrlIntoDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "test_database")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            var shortUrl = new ShortUrl
            {
                OriginalUrl = "https://www.google.com/23456ytgfderf",
                ShortenUrl = "https://www.google.com/abc",
                CreatedByUserId = "1",
                Description = "Google",
                CreatedDate = DateTime.Now
            };
                
            // Act
            context.ShortUrls.Add(shortUrl);
            context.SaveChanges();

            // Assert
            Assert.Single(context.ShortUrls.ToList());
        }
    }

    [Fact]
    public async Task CanRetrieveShortUrlByIdFromDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "test_database")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            var shortUrl = new ShortUrl
            {
                OriginalUrl = "https://www.google.com/23456ytgfderf",
                ShortenUrl = "https://www.google.com/abc",
                CreatedByUserId = "1",
                Description = "Google",
                CreatedDate = DateTime.Now
            };

            context.ShortUrls.Add(shortUrl);
            await context.SaveChangesAsync();

            // Act
            var retrievedShortUrl = await context.ShortUrls.FindAsync(shortUrl.Id);

            // Assert
            Assert.NotNull(retrievedShortUrl);
            Assert.Equal(shortUrl.OriginalUrl, retrievedShortUrl.OriginalUrl);
        }
    }

    [Fact]
    public async Task CanRemoveShortUrlByIdFromDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "test_database")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            var shortUrl = new ShortUrl
            {
                OriginalUrl = "https://www.google.com/23456ytgfderf",
                ShortenUrl = "https://www.google.com/abc",
                CreatedByUserId = "1",
                Description = "Google",
                CreatedDate = DateTime.Now
            };

            context.ShortUrls.Add(shortUrl);
            await context.SaveChangesAsync();

            // Act
            context.ShortUrls.Remove(shortUrl);
            await context.SaveChangesAsync();

            // Assert
            Assert.Empty(context.ShortUrls);
        }
    }
}

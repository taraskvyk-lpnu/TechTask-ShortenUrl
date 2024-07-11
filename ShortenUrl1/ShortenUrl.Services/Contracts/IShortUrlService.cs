using ShortenUrl.Infrastructure.Dtos;

namespace ShortenUrl.Services.Contracts;

public interface IShortUrlService
{
    Task<IEnumerable<ShortUrlDto>> GetAllShortUrls();
    Task<ShortUrlDto> GetShortUrlById(int id);
    Task<ShortUrlDto> CreateShortUrl(CreateShortUrlDto createShortUrlDto);
    Task<ShortUrlDto> UpdateShortUrl(UpdateShortUrlDto updateShortUrlDto);
    Task DeleteShortUrl(int id, int userId);
}
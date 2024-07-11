namespace ShortenUrl.Infrastructure.Dtos;

public class CreateShortUrlDto
{
    public string OriginalUrl { get; set; }
    public int CreatedByUserId { get; set; }
    public string Description { get; set; }
}
namespace ShortenUrl.Infrastructure.Dtos;

public class ShortUrlDto
{
    public int Id { get; set; }
    public string OriginalUrl { get; set; }
    public string ShortenUrl { get; set; }
    public int CreatedByUserId { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatorName { get; set; }
}
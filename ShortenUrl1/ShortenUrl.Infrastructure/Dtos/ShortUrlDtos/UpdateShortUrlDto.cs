namespace ShortenUrl.Infrastructure.Dtos;

public class UpdateShortUrlDto
{
    public int Id { get; set; }
    public int CreatedByUserId { get; set; }
    public string Description { get; set; }
}
namespace ShortenUrl.Infrastructure.Exceptions;

public class DuplicateShortUrlException : Exception
{
    public DuplicateShortUrlException(string message) : base(message)
    {
    }
}
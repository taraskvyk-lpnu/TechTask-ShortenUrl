namespace ShortenUrl.Infrastructure.Exceptions;

public class NullShortUrlException : Exception
{
    public NullShortUrlException(string message) : base(message)
    {
    }
}
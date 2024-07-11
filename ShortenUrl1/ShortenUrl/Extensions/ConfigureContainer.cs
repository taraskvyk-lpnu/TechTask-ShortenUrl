using Microsoft.AspNetCore.Builder;
using ShortenUrl.Infrastructure.Middlewares;

namespace ShortenUrl.Infrastructure.Extensions;

public static class ConfigureContainer
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionHandler>();
    }
}
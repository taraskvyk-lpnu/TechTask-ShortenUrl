using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShortenUrl.Domain.Auth;
using ShortenUrl.Domain.Entities;
using ShortenUrl.Domain.Enum;
using ShortenUrl.Infrastructure.Mappers;
using ShortenUrl.Persistence;
using ShortenUrl.Persistence.Repository.Contracts;
using ShortenUrl.Persistence.Repository.Implementations;
using ShortenUrl.Services.Contracts;
using ShortenUrl.Services.Implementations;

namespace ShortenUrl.Infrastructure.Extensions;

public static class ConfigureServiceContainer
{
    public static void AddDbContext(this IServiceCollection serviceCollection,
         IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(configuration["ConnectionStrings:DefaultDbConnection"]));
    }
    
    public static void AddScopedServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        serviceCollection.AddScoped<IRepository<ShortUrl>, Repository<ShortUrl>>();
        serviceCollection.AddScoped<IShortUrlRepository, ShortUrlRepository>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        
        serviceCollection.AddScoped<IAccountService, AccountService>();
        serviceCollection.AddScoped<IShortUrlService, ShortUrlService>();
    }
    
    public static void AddJwtToken(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });
    }

    public static void AddAutoMappers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(ShortUrlProfile));
    }
    
    public static void AddIdentity(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
    
    public static void AddCustomCors(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(options =>
            options.AddPolicy("AllowAll", builder => 
                builder
                    .AllowAnyOrigin()
                    .WithMethods("GET", "POST", "PUT", "DELETE")
                    .AllowAnyHeader()
                )
            );
    }
}
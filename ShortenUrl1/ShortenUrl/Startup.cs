using Microsoft.OpenApi.Models;
using ShortenUrl.Infrastructure.Extensions;

namespace ShortenUrl1;

public class Startup
{
    private readonly IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        
        services.AddDbContext(Configuration);
        
        services.AddScopedServices();
        
        services.AddJwtToken(Configuration);

        services.AddAutoMappers();

        services.AddSwaggerGen(options => 
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore API", Version = "v1" });
        });

        services.AddCustomCors();
        services.AddIdentity();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore API V1");
            options.RoutePrefix = ""; 
        });
        
        app.ConfigureCustomExceptionMiddleware();
        app.UseHttpsRedirection();
        
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("AllowAll");
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
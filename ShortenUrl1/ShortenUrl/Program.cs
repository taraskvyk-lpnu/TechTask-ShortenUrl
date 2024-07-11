namespace ShortenUrl1;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(options =>
                options.UseStartup<Startup>());
    }
}
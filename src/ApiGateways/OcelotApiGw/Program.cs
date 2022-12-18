using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotApiGw
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);

            builder.Services.AddOcelot().AddCacheManager(x =>
            {
                x.WithDictionaryHandle();
            });

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.UseOcelot().Wait();

            app.Run();
        }
    }
}
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration ((hostContext, config) =>
{
    config.AddJsonFile($"ocelot{hostContext.HostingEnvironment.EnvironmentName}.json", true, true);
});
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddOcelot().AddCacheManager(s => s.WithDictionaryHandle());

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

await app.UseOcelot();

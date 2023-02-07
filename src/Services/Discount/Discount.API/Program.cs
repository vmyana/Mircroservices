using Discount.API.Data;
using Discount.API.Infrastructure;
using Discount.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
});
builder.Services.AddSingleton<ApplicationInitializer>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using (var scope = scopeFactory.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<ApplicationInitializer>();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await initializer.InitializeAsync(CancellationToken.None).ConfigureAwait(false);
    await initializer.SeedData(dbContext);
}

app.Run();

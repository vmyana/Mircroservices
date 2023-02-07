using Discount.API.Data;
using Discount.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Infrastructure
{
    public class ApplicationInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ApplicationInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                using var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await db.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task SeedData(ApplicationDbContext context)
        {
            if (! context.Coupons.Any())
            {
                var coupons = new List<Coupon>
                {
                    new Coupon { ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150},
                    new Coupon { ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100}
                };

                context.AddRange(coupons);
                await context.SaveChangesAsync();
            }
        }
    }
}

using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ApplicationDbContext _context;

        public DiscountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();

            var savedCoupon = await GetDiscount(coupon.ProductName);
            if (savedCoupon.ProductName == coupon.ProductName)
                return true;

            return false;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var coupon = await _context.Coupons.Where(p => p.ProductName == productName).FirstOrDefaultAsync();
            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            var coupon = await _context.Coupons.Where(p => p.ProductName == productName).FirstOrDefaultAsync();
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            _context.Coupons.Update(coupon);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

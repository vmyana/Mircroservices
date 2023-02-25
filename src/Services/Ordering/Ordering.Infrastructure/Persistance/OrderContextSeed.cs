using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistance
{
    public class OrderContextSeed
    {
        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() 
                {
                    UserName = "vmyana", 
                    FirstName = "Vamshi", 
                    LastName = "Krishna", 
                    EmailAddress = "vamshikrishna4638@gmail.com", 
                    AddressLine = "Hyderabad", 
                    Country = "India", 
                    TotalPrice = 350 
                }
            };
        }

        public static async Task SeedAsync(OrderContext orderContext)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
            }
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vending.Repositories.Entities;

namespace Vending.Repositories.Context
{
    public class VendingDbContext : DbContext
    {
        public VendingDbContext(DbContextOptions<VendingDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CoinStackEntity> Currencies { get; set; }
        public DbSet<PurchaseWorkflowEntity> PurchaseWorkflows { get; set; }
    }

    public static class DatabaseSetup
    {
        public static void InitializeDatabase(this IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<VendingDbContext>())
            {
                context.Products.AddRange(
                    new ProductEntity { Name = "Tea", Price = 130, Portions = 10 },
                    new ProductEntity { Name = "Espresso", Price = 180, Portions = 20 },
                    new ProductEntity { Name = "Juice", Price = 180, Portions = 20 },
                    new ProductEntity { Name = "Chicken soup", Price = 180, Portions = 20 });

                context.Currencies.AddRange(
                    new CoinStackEntity { Value = 10, Count = 100 },
                    new CoinStackEntity { Value = 20, Count = 100 },
                    new CoinStackEntity { Value = 50, Count = 100 },
                    new CoinStackEntity { Value = 100, Count = 100 });

                context.SaveChanges();
            }
        }
    }
}

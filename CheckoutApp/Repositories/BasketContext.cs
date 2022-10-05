using CheckoutApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CheckoutApp.Repositories
{
    public class BasketContext: DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public BasketContext(DbContextOptions<BasketContext> options)
            : base(options)
                { }
   
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}

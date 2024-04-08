using Microsoft.EntityFrameworkCore;

namespace brickit.Models
{
    public class LegoDbContext: DbContext
    {
        public LegoDbContext(DbContextOptions<LegoDbContext> options) : base(options){}

        public DbSet<Customer> customers { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<lineItem> lineItems { get; set; }

    }
}

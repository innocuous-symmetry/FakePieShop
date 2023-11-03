using Microsoft.EntityFrameworkCore;

namespace FakePieShop.Models
{
    public class FakePieShopDbContext : DbContext
    {
        // no further configuration needed for this constructor; all is handled by the superclass
        public FakePieShopDbContext(DbContextOptions<FakePieShopDbContext> options) : base(options) 
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> Pies { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}

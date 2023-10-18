namespace ShoppingApp.Models;

using Microsoft.EntityFrameworkCore;

public class PieShopDBContext : DbContext
{
    public PieShopDBContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Pie> Pies { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

}

using System.Data.Entity;
using SOPGraphQL;

using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }
    
    public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
    
    public Microsoft.EntityFrameworkCore.DbSet<Order> Orders { get; set; }
    
    public Microsoft.EntityFrameworkCore.DbSet<OrderItem> OrderItems { get; set; }
    
    public Microsoft.EntityFrameworkCore.DbSet<MenuItem> MenuItems { get; set; }
    
    public Microsoft.EntityFrameworkCore.DbSet<Promotion> Promotions { get; set; }
}

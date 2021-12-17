using Microsoft.EntityFrameworkCore;
using Tionit.MassTransit.Consumer.Models;

namespace Tionit.MassTransit.API.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }
        
        public DbSet<CustomerModel> Customers { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Tionit.MassTransit.Consumer.Models;
 

namespace Tionit.MassTransit.Consumer.Entities
{
    public class DataContext : DbContext
    {
       
        //public DataContext()
        //{
        //    //
        //    Database.EnsureDeleted();
        //    Database.EnsureCreated();
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"host=localhost;port=5432;database=RQDB;username=postgres;password=saadmin123");
        }
        

        public DbSet<CustomerModel>  Customers { get; set; }

    }
}

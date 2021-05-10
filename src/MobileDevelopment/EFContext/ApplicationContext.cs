using Microsoft.EntityFrameworkCore;
using MobileDevelopment.Models;

namespace MobileDevelopment.EFContext
{
    public class ApplicationContext : DbContext
    {
        private readonly string _connection;

        public DbSet<Book> Books { get; set; }
        public DbSet<Gallery> Galleries { get; set; }

        public ApplicationContext(string connection)
        {
            _connection = connection;
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_connection}");
        }
    }
}
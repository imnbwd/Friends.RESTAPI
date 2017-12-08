using Friends.Models;
using Microsoft.EntityFrameworkCore;

namespace Friends.Data
{
    public class FriendDbContext : DbContext
    {
        public DbSet<Friend> Friends { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=./Friends.db");
            //            Database.EnsureCreated();
        }
    }
}
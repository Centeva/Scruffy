using Microsoft.EntityFrameworkCore;

namespace ScruffyNetCore
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class MyDb : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MyDb()
        {
        }

        public MyDb(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("DataSource=:memory:");
        //}
    }
}
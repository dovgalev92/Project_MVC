using Microsoft.EntityFrameworkCore;
using Project_MVC.Entity;
using Project_MVC.Entity.AccessUsers;

namespace Project_MVC.Context_DataBase
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb>options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<DataVisits> DataVisits { get; set; }
        public DbSet<LogInUser> LogInUsers { get; set; }
    }
}

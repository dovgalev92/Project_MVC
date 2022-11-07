using Microsoft.EntityFrameworkCore;
using Project_MVC.Entity;

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
        public DbSet<Hause_Details> Hause_Details { get; set; }
    }
}

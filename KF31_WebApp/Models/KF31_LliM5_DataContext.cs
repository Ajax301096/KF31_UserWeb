using Microsoft.EntityFrameworkCore;

namespace KF31_WebApp.Models
{
    public class KF31_LliM5_DataContext:DbContext 
    {
        public KF31_LliM5_DataContext(DbContextOptions<KF31_LliM5_DataContext> options) : base(options){}
        public DbSet<Book> Books { get; set; }

        public DbSet<Member> Members { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Order>()
        //        .HasKey(od => new { od.No, od.Num });
        //}
    }
}

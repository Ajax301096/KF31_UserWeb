using Microsoft.EntityFrameworkCore;

namespace KF31_WebApp.Models
{
    public class KF31_LliM5_DataContext:DbContext 
    {
        public KF31_LliM5_DataContext(DbContextOptions<KF31_LliM5_DataContext> options) : base(options){}
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Libraty> Libraties { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Yoyaku> Yoyakus { get; set; }
      
    }
}

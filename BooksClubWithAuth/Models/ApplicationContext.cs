using Microsoft.EntityFrameworkCore;

namespace BooksClubWithAuth.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Favorite> Favorites { get; set; } 
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorite>().HasMany(b => b.Books);
        }
    }
}

using Humanizer.Localisation;
using INFT3050.Models;
using Microsoft.EntityFrameworkCore;

namespace INFT3050.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<ContactMessage> Messages { get; set; } = null;
        public DbSet<Product> Products { get; set; } = null;

        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = "A", Name = "Table", Room = "Living-Room" },
                new Category { CategoryId = "B", Name = "Sofa", Room = "Living-Room" },
                new Category { CategoryId = "C", Name = "TV-Consoles" , Room = "Living-Room" },
                new Category { CategoryId = "D", Name = "Shelves", Room = "Living-Room" },
                new Category { CategoryId = "E", Name = "Headrest" , Room = "Living-Room" },
                new Category { CategoryId = "F", Name = "Stools", Room = "Dining-Room" },
                new Category { CategoryId = "G", Name = "Cabinets", Room = "Dining-Room" },
                new Category { CategoryId = "H", Name = "Dining-Table", Room = "Dining-Room" },
                new Category { CategoryId = "I", Name = "Chairs", Room = "Dining-Room" },
                new Category { CategoryId = "J", Name = "Beds", Room = "Beds" },
                new Category { CategoryId = "K", Name = "Bedside-Table", Room = "Beds" },
                new Category { CategoryId = "L", Name = "Mirror", Room = "Beds" },
                new Category { CategoryId = "M", Name = "Cloth-Hanger-Racks", Room = "Beds" },
                new Category { CategoryId = "Q", Name = "Study-Table", Room = "Beds" }
            );

        }
    }
}

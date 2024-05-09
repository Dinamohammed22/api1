using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api1.Models
{
    public class StoreContext:IdentityDbContext<ApplicationUser>
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Product>().HasData(new Product { id = 1, Name = "Strawberry" , Description = "Fruit"});
            modelBuilder.Entity<Product>().HasData(new Product { id = 2, Name = "Banana" , Description = "Fruit"});
            modelBuilder.Entity<Product>().HasData(new Product { id = 3, Name = "Tomato" , Description = "Vegitable"});
            modelBuilder.Entity<Product>().HasData(new Product { id = 4, Name = "Carrot" , Description = "Vegitable" });
            modelBuilder.Entity<Product>().HasData(new Product { id = 5, Name = "Kiwi" , Description = "Fruit"});
            modelBuilder.Entity<Product>().HasData(new Product { id = 6, Name = "Cucumber" , Description = "Vegitable" });


            modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Vegetables" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 2, Name = "Fruits" });

            base.OnModelCreating(modelBuilder);
        }

    }
}

using LearningApp.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearningApp.DataAccess.Data
{

    public class ApplictionDBContex: IdentityDbContext<IdentityUser>
    {
        public DbSet <Category> Categories { get; set; }
        public DbSet <Product> Products { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public ApplictionDBContex(DbContextOptions<ApplictionDBContex> options) :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1,Name="Action",DisplayOrder=1},
                new Category { Id = 2, Name = "SiCfi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product { Id=1,
                Title="Somethiong",
                Author="Prasent",
                ISBN="SW112323124",
                ListPrice=99,
                Price = 90,
                Price50=80,
                Price100=100,
                CategoryId=1,
                ImageUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Title = "Somethiofsafng",
                    Author = "Praseasgsant",
                    ISBN = "SW112323124",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 80,
                    Price100 = 100,
                    CategoryId = 2,
                    ImageUrl=""
                },
                new Product
                {
                    Id = 3,
                    Title = "Somethiong",
                    Author = "Prasent",
                    ISBN = "SW112323124",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 80,
                    Price100 = 100,
                    CategoryId = 3,
                    ImageUrl=""
                });
        }


    }
}

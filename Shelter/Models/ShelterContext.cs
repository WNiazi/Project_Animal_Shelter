using Microsoft.EntityFrameworkCore;

namespace Shelter.Models
{
  public class ShelterContext : DbContext
  {
    public ShelterContext(DbContextOptions<ShelterContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Cat>()
        .HasData(
          new Cat { CatId = 1, Name = "Bob", Gender = "Male", Type = "furry", Age = 1 },
          new Cat { CatId = 2, Name = "Tod", Gender = "Female", Type = "very furry", Age = 2 },
          new Cat { CatId = 3, Name = "Sod", Gender = "Male", Type = "not furry", Age = 3 }
          );
      builder.Entity<Dog>()
        .HasData(
          new Dog { DogId = 1, Name = "Cat", Gender = "Female", Type = "not at all furry", Age = 1 },
          new Dog { DogId = 2, Name = "Mat", Gender = "Male", Type = "a little fur", Age = 2 },
          new Dog { DogId = 3, Name = "Sat", Gender = "Female", Type = "bald", Age = 3 }
        );
    }

    public DbSet<Cat> Cats { get; set; }
    public DbSet<Dog> Dogs { get; set; }
  }
}
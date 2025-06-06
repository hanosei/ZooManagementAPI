using Microsoft.EntityFrameworkCore;
using ZooManagementAPI.Models;

namespace ZooManagementAPI
{
    public class ZooManagementAPIContext : DbContext
    {
        public ZooManagementAPIContext(DbContextOptions<ZooManagementAPIContext> options) : base (options){}
        
        // Put all the tables you want in your database here
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Enclosure> Enclosures { get; set; }
        public DbSet<Zookeeper> Zookeepers { get; set; }
        public DbSet<ZookeeperAndAnimal> ZookeeperAndAnimals { get; set; }
        public DbSet<ZookeeperAndEnclosure> ZookeeperAndEnclosures { get; set; }
    
        public void SeedEnclosures() //need to work on this: create an instance of Context t call this method on in program.cs
        {
            if (Enclosures.Any()) return;

            var enclosures = new List<Enclosure> {
                new Enclosure {Name = "Lion Enclosure", MaxCapacity = 10},
                new Enclosure {Name = "Fish Enclosure", MaxCapacity = 50},
                new Enclosure {Name = "Bird Enclosure", MaxCapacity = 30},
                new Enclosure {Name = "Monkey Enclosure", MaxCapacity = 15},
                new Enclosure {Name = "Giraffe Enclosure", MaxCapacity = 10},
                new Enclosure {Name = "Elephant Enclosure", MaxCapacity = 5},
                new Enclosure {Name = "Tiger Enclosure", MaxCapacity = 10},
                new Enclosure {Name = "Farm Animal Enclosure", MaxCapacity = 20},
                new Enclosure {Name = "Meerkat Enclosure", MaxCapacity = 20},
            }

            Enclosures.AddRange(enclosures);
            SaveChanges();
        }
    
    }
};
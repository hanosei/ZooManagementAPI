using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using ZooManagementAPI.Models;

namespace ZooManagementAPI
{
    public class ZooManagementAPIContext : DbContext
    {
        public ZooManagementAPIContext(DbContextOptions<ZooManagementAPIContext> options) : base(options) { }

        // Put all the tables you want in your database here
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Enclosure> Enclosures { get; set; }
        public DbSet<Zookeeper> Zookeepers { get; set; }
        public DbSet<ZookeeperAndAnimal> ZookeeperAndAnimals { get; set; }
        public DbSet<ZookeeperAndEnclosure> ZookeeperAndEnclosures { get; set; }

        public void SeedEnclosures()
        {
            if (Enclosures.Any()) return;

            var enclosures = new List<Enclosure> {
                new Enclosure {Name = "Lion Enclosure", MaxCapacity = 10},
                new Enclosure {Name = "Fish Enclosure", MaxCapacity = 50},
                new Enclosure {Name = "Aviary Enclosure", MaxCapacity = 50},
                new Enclosure {Name = "Monkey Enclosure", MaxCapacity = 15},
                new Enclosure {Name = "Giraffe Enclosure", MaxCapacity = 6},
                new Enclosure {Name = "Elephant Enclosure", MaxCapacity = 5},
                new Enclosure {Name = "Tiger Enclosure", MaxCapacity = 10},
                new Enclosure {Name = "Farm Animal Enclosure", MaxCapacity = 20},
                new Enclosure {Name = "Hippo Enclosure", MaxCapacity = 20},
                new Enclosure {Name = "Reptile Enclosure", MaxCapacity = 40}
            };

            Enclosures.AddRange(enclosures);
            SaveChanges();
        }
        
           public void SeedAnimals()
        {
            if (Animals.Any()) return;

            var animals = new List<Animal>();
            var random = new Random();

            var animalDictionary = new Dictionary<string, string>
            {
                {"Lion", "Mammal" },
                {"Tiger", "Mammal" },
                {"Giraffe", "Mammal" },
                {"Elephant", "Mammal" },
                {"Monkey", "Mammal" },
                {"Owl", "Bird"},
                {"Fish", "Fish" },
                {"Hippo","Mammal" },
                {"Crocodile", "Reptile"},
                {"Dolphin", "Fish"},
                {"Parrot", "Bird"},
                {"Iguana", "Reptile"},
                {"Snake", "Reptile"},
                {"Sheep", "Mammal"},
                {"Goat", "Mammal"}
            };

            var animalEnclosure = new Dictionary<string, string>
            {
                {"Lion", "Lion Enclosure" },
                {"Tiger", "Tiger Enclosure" },
                {"Giraffe", "Giraffe Enclosure" },
                {"Elephant", "Elephant Enclosure" },
                {"Monkey", "Monkey Enclosure" },
                
                { "Parrot", "Aviary Enclosure"},
                { "Owl", "Aviary Enclosure"},
                
                {"Hippo","Hippo Enclosure" },
                
                {"Fish", "Fish Enclosure" },
                {"Dolphin", "Fish Enclosure"},
                
                { "Crocodile", "Reptile Enclosure"},
                { "Iguana", "Reptile Enclosure"},
                {"Snake", "Reptile Enclosure"},
                
                { "Sheep", "Farm Animal Enclosure"},
                {"Goat", "Farm Animal Enclosure"}
            };


            var enclosures = Enclosures.ToDictionary(enclosures => enclosures.Name, enclosures => enclosures.EnclosureId);
            string[] animalList = animalDictionary.Keys.ToArray();
            

            for(int i = 0; i <=100; i++)
            {
                string randomAnimal = animalList.ElementAt(random.Next(0,animalList.Length));
                string randomAnimalEnclosure = animalEnclosure[randomAnimal];
                int randomAnimalEnclosureId = enclosures[randomAnimalEnclosure];
                DateTime dateOfBirth = DateTime.Now.AddYears(-random.Next(1 , 10));
                DateTime dateAcquired = dateOfBirth.AddYears(random.Next(0 , 5));
             

                if (dateAcquired > DateTime.Now)
                {
                    dateAcquired = DateTime.Now;
                }

                DateTime? dateLeft = null;
                string status;
                string? transferredToZoo = null;

                int statusChooser = random.Next(0, 3);
                if (statusChooser == 1)
                {
                    status = "Active";
                }
                else if (statusChooser == 2)
                {
                    dateLeft = dateAcquired.AddYears(random.Next(1, 3));
                    if (dateLeft > DateTime.Now) dateLeft = DateTime.Now;
                    status = "Deceased";
                }
                else
                {
                    dateLeft = dateAcquired.AddYears(random.Next(1, 3));
                    if (dateLeft > DateTime.Now) dateLeft = DateTime.Now;
                    status = "Transferred";
                    transferredToZoo = (i % 2 == 0) ? "Central Park Zoo" : "San Diego Zoo";
                }

                Animals.Add(new Animal
                {
                    Name = $"{randomAnimal}_{i}",
                    Species = randomAnimal,
                    Classification = animalDictionary[randomAnimal],
                    EnclosureId = randomAnimalEnclosureId,
                    Sex = (i % 2 == 0) ? "Male" : "Female",
                    DateOfBirth = dateOfBirth,
                    DateAcquired = dateAcquired,
                    DateLeft = dateLeft,
                    AnimalStatus = status,
                    TransferredToZoo = transferredToZoo
                });
            }

            
            SaveChanges();
        }
    
    }
};
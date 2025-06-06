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
    }
};
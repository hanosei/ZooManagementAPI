namespace ZooManagementAPI.Models
{
    public class ZookeeperAndAnimal
    {
        public int ZookeeperAndAnimalId { get; set; }

        public int ZookeeperId { get; set; }
        public Zookeeper Zookeeper {get; set;}

        public int AnimalId { get; set; }
        public Animal Animal {get; set;}
        
    }
}
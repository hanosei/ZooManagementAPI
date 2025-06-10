namespace ZooManagementAPI.Models
{
    public class Zookeeper
    {
        public int ZookeeperId { get; set; }
        public string Name { get; set; }


        public ICollection<ZookeeperAndEnclosure> ZookeeperAndEnclosures { get; set; } = new List<ZookeeperAndEnclosure>();
        public ICollection<ZookeeperAndAnimal> ZookeeperAndAnimals { get; set; } = new List<ZookeeperAndAnimal>();
        
    }
}
namespace ZooManagementAPI.Models
{
    public class ZookeeperAndEnclosure
    {
        public int ZookeeperAndEnclosureId { get; set; }

        public int ZookeeperId { get; set; }
        public Zookeeper Zookeeper {get; set;}

        public int EnclosureId { get; set; }
        public Enclosure Enclosure {get; set;}
        
    }
}
namespace ZooManagementAPI.Models
{
    public class Enclosure
    {
        public int EnclosureId { get; set; }
        public string Name { get; set; }
        public int MaxCapacity { get; set; }

        public ICollection<ZookeeperAndEnclosure> ZookeeperAndEnclosures { get; set; } = new List<ZookeeperAndEnclosure>();
        
    }
}
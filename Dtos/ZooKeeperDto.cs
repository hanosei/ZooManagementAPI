    namespace ZooManagementAPI.Dtos
    {
    public class ZookeeperDto
    {
        public int ZooKeeperId { get; set; }
        public string ZooKeeperName { get; set; }

        public List<AnimalDto> Animals { get; set; }
        
        public List<EnclosureDto> Enclosures { get; set; }

        }
    }
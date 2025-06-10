namespace ZooManagementAPI.Dtos
{
    public class CreateZookeeperDto
    {
        public string ZookeeperName { get; set; }
        public List<int> EnclosureIds { get; set; }
        public List<int> AnimalIds { get; set; }

    }
}
namespace ZooManagementAPI.Models
{
    public class Animal
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Classification { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateAcquired { get; set; }
        public DateTime? DateLeft { get; set; }
        public string? TransferredToZoo { get; set; } //name of zoo it went to if transferred
        public string? AnimalStatus { get; set; } //to check if anumal is dead, transferred , or active in zoo
        public int EnclosureId { get; set; }
        public Enclosure Enclosure { get; set; }
        
        public ICollection<ZookeeperAndAnimal> ZookeeperAndAnimals { get; set; } = new List<ZookeeperAndAnimal>();

    }
}
    namespace ZooManagementAPI.Dtos
    {
        public class CreateAnimalDto
        {
            public int AnimalId { get; set; }
            public string Name { get; set; }
            public string Species { get; set; }
            public string Classification { get; set; }
            public string Sex { get; set; }
            public DateTime DateOfBirth { get; set; }
            public DateTime DateAcquired { get; set; }
            public int EnclosureId  { get; set; }

        }
    }
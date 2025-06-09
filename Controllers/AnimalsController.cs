using ZooManagementAPI.Models;
using ZooManagementAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ZooManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly ZooManagementAPIContext _context;

        public AnimalsController(ZooManagementAPIContext context)
        {
            _context = context;
        }

        [HttpGet("{AnimalId}")]
        public IActionResult GetAnimalById(int AnimalId)
        {
            var animal = _context.Animals.Find(AnimalId);

            if (animal == null) return NotFound();

            return Ok(animal);
        }

        [HttpPost]
        public IActionResult AddAnimal([FromBody] CreateAnimalDto animalDto)
        {
            if (animalDto == null) return BadRequest();

            var animal = new Animal
            {
                Name = animalDto.Name,
                Species = animalDto.Species,
                Classification = animalDto.Classification,
                Sex = animalDto.Sex,
                DateOfBirth = animalDto.DateOfBirth,
                DateAcquired = animalDto.DateAcquired,
                EnclosureId = animalDto.EnclosureId
            };

            _context.Animals.Add(animal);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAnimalById), new { AnimalId = animal.AnimalId }, animal);
        }


        [HttpGet("AnimalType/{AnimalType}")]
        public IActionResult GetAnimalsBySpecies(string AnimalType)
        {
            if (string.IsNullOrWhiteSpace(AnimalType)) return BadRequest("Enter a species");

            List<Animal> animals = _context.Animals.Where(animal => animal.Species == AnimalType).ToList();

            if (!animals.Any()) return NotFound("There are no animals found for that species");

            return Ok(animals);
        }

        [HttpGet("Search")]
        public IActionResult SearchAnimals(string? species,
                                        string? classification,
                                        string? name,
                                        int? age,
                                        DateOnly? dateAcquired,
                                        int pageSize = 5,
                                        int pageNumber = 1
                                        )
        {
            
            var queryAnimals = _context.Animals.AsQueryable();
            if (!string.IsNullOrEmpty(species)) {
                queryAnimals = queryAnimals.Where(animals => animals.Species.ToLower().Contains(species.ToLower()));
            }
             if (!string.IsNullOrEmpty(classification)) {
                queryAnimals = queryAnimals.Where(animals => animals.Classification.ToLower().Contains(classification.ToLower()));
            }
             if (!string.IsNullOrEmpty(name)) {
                queryAnimals = queryAnimals.Where(animals => animals.Name.ToLower().Contains(name.ToLower()));
            }
             if (age.HasValue) {
                int currentYear = DateTime.Now.Year;
                queryAnimals = queryAnimals.Where(animals => (currentYear - animals.DateOfBirth.Year) == age);
            }
             if (dateAcquired.HasValue) {
                queryAnimals = queryAnimals.Where(animals => DateOnly.FromDateTime(animals.DateAcquired) == dateAcquired.Value);
            }

            int totalQuery = queryAnimals.Count();
            if (totalQuery > 0)
            {
                int totalPagesNeeded = (int)Math.Ceiling((double)totalQuery / pageSize);
                int skipRecords = totalQuery / totalPagesNeeded * (pageNumber - 1);
                var chosenPage = queryAnimals.Skip(skipRecords).Take(pageSize).ToList();

                var response = new
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = totalPagesNeeded,
                    TotalRecords = totalQuery,
                    Animals = chosenPage
                };

                return Ok(response);
            }
            else
            {
                var response = new
                {
                    message = "No animals match your search. Try again"
                };
                
                return Ok(response);
            }
        }
    }
}
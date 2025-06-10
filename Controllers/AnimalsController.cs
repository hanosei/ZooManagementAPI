using ZooManagementAPI.Models;
using ZooManagementAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            //count of animals in animaldto.enclosure id from Animals table
            //max capacity < count of animals 

            var enclosure = _context.Enclosures.FirstOrDefault(animals => animals.EnclosureId == animalDto.EnclosureId);
            var currentAnimalsInEnclosure = _context.Animals.Count(animals => animals.EnclosureId == animalDto.EnclosureId && animals.AnimalStatus == "Active");
            if (enclosure == null) return NotFound("Enclosure does not exist");
            
            if (currentAnimalsInEnclosure >= enclosure.MaxCapacity)
            {
                return BadRequest("The enclosure has reached maximum capacity of active animals.");
            }

            var animal = new Animal
            {
                Name = animalDto.Name,
                Species = animalDto.Species,
                Classification = animalDto.Classification,
                Sex = animalDto.Sex,
                DateOfBirth = animalDto.DateOfBirth,
                DateAcquired = animalDto.DateAcquired,
                EnclosureId = animalDto.EnclosureId,
                AnimalStatus = "Active"
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
                                        string? enclosure,
                                        int pageSize = 5,
                                        int pageNumber = 1,
                                        string? orderResults = "species",
                                        bool descending = false
                                        
                                        )
        {

            var queryAnimals = _context.Animals.AsQueryable();
            if (!string.IsNullOrEmpty(species))
            {
                queryAnimals = queryAnimals.Where(animals => animals.Species.ToLower().Contains(species.ToLower()));
            }
            if (!string.IsNullOrEmpty(classification))
            {
                queryAnimals = queryAnimals.Where(animals => animals.Classification.ToLower().Contains(classification.ToLower()));
            }
            if (!string.IsNullOrEmpty(name))
            {
                queryAnimals = queryAnimals.Where(animals => animals.Name.ToLower().Contains(name.ToLower()));
            }
            if (!string.IsNullOrEmpty(enclosure))
            {
                queryAnimals = queryAnimals.Where(animals => animals.Enclosure.Name.ToLower().Contains(enclosure.ToLower()));
            }
            if (age.HasValue)
            {
                int currentYear = DateTime.Now.Year;
                queryAnimals = queryAnimals.Where(animals => (currentYear - animals.DateOfBirth.Year) == age);
            }
            if (dateAcquired.HasValue)
            {
                queryAnimals = queryAnimals.Where(animals => DateOnly.FromDateTime(animals.DateAcquired) == dateAcquired.Value);
            }

            var validOrderOptions = new[] { "name", "species", "classification", "age", "date acquired", "enclosure"};
            orderResults = validOrderOptions.Contains(orderResults?.ToLower()) ? orderResults.ToLower() : "species";
            switch (orderResults)
            {
                case "name":
                    queryAnimals = descending? queryAnimals.OrderByDescending(animals => animals.Name) : queryAnimals.OrderBy(animals => animals.Name);
                    break;
                case "classification":
                    queryAnimals = descending? queryAnimals.OrderByDescending(animals => animals.Classification) : queryAnimals.OrderBy(animals => animals.Classification);
                    break;
                case "enclosure":
                    queryAnimals = descending? queryAnimals.OrderByDescending(animals => animals.Enclosure.Name).ThenByDescending(animals => animals.Name)
                                    : queryAnimals.OrderBy(animals => animals.Enclosure.Name).ThenBy(animals => animals.Name);
                    break;
                case "age":
                    queryAnimals = descending? queryAnimals.OrderByDescending(animals => animals.DateOfBirth): queryAnimals.OrderBy(animals => animals.DateOfBirth);
                    break;  
                case "date acquired":
                    queryAnimals = descending? queryAnimals.OrderByDescending(animals => animals.DateAcquired): queryAnimals.OrderBy(animals => animals.DateAcquired);
                    break;
                default:
                    queryAnimals = descending? queryAnimals.OrderByDescending(animals => animals.Species): queryAnimals.OrderBy(animals => animals.Species);
                    break;
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
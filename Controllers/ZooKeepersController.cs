using ZooManagementAPI.Models;
using ZooManagementAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ZooManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZookeepersController : ControllerBase
    {
        private readonly ZooManagementAPIContext _context;

        public ZookeepersController(ZooManagementAPIContext context)
        {
            _context = context;
        }

        [HttpGet("{ZookeeperId}")]
        public IActionResult GetZookeeperById(int ZookeeperId)
        {
            var zookeeper = _context.Zookeepers
                            .Include(zookeeper => zookeeper.ZookeeperAndEnclosures).ThenInclude(zookeeperEnclosures => zookeeperEnclosures.Enclosure)
                            .Include(zookeeper => zookeeper.ZookeeperAndAnimals).ThenInclude(ZookeeperAnimals => ZookeeperAnimals.Animal)
                            .FirstOrDefault(zookeeper => zookeeper.ZookeeperId == ZookeeperId);

            if (zookeeper == null) return NotFound();

            var zookeeperEnclosures = zookeeper.ZookeeperAndEnclosures.Select(zookeeeperEnclosures =>
                                        new EnclosureDto
                                        {
                                            EnclosureId = zookeeeperEnclosures.Enclosure.EnclosureId,
                                            EnclosureName = zookeeeperEnclosures.Enclosure.Name
                                        }).ToList();
            var zookeeperAnimals = zookeeper.ZookeeperAndAnimals.Select(zookeeeperAnimals =>
                                        new AnimalDto
                                        {
                                            AnimalId = zookeeeperAnimals.Animal.AnimalId,
                                            AnimalName = zookeeeperAnimals.Animal.Name,
                                            AnimalSpecies = zookeeeperAnimals.Animal.Species,
                                            AnimalClassifiction = zookeeeperAnimals.Animal.Classification

                                        }).ToList();

            var zookeeperDetails = new ZookeeperDto
            {
                ZooKeeperId = zookeeper.ZookeeperId,
                ZooKeeperName = zookeeper.Name,
                Enclosures = zookeeperEnclosures,
                Animals = zookeeperAnimals
            };

            return Ok(zookeeperDetails);
        }
        
        // [HttpPost]
        // public IActionResult AddZookeeper()
        // {
           
        // }
    
    }
}
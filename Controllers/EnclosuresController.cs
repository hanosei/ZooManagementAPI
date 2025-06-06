using ZooManagementAPI.Models;
using ZooManagementAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ZooManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnclosuresController : ControllerBase
    {
        private readonly ZooManagementAPIContext _context;

        public EnclosuresController(ZooManagementAPIContext context)
        {
            _context = context;
        }


    }
}
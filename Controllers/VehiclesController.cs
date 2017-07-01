using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Controllers.Resources;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private IMapper _mapper;

        public VehiclesController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult CreateVehicle([FromBody]VehicleResource vehicleResource)
        {
            //Mapping API Resource to domain object
            var vehicle = _mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            //This will get serialized as JSON object and return HTTP status code 200
            return Ok(vehicle);
        }
    }
}

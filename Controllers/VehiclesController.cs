using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Controllers.Resources;
using ShoppingCart.Models;
using ShoppingCart.Persistence;

namespace ShoppingCart.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private IMapper _mapper;
        private ShoppingCartDbContext _context;

        public VehiclesController(IMapper mapper, ShoppingCartDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody]SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Custom Error Handling
            var model = await _context.Models.FindAsync(saveVehicleResource.ModelId);
            if (model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid ModelId");
                return BadRequest(ModelState);
            }
            //Mapping API Resource to domain object
            var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource);
            vehicle.LastUpdate = DateTime.Now;
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            //This will get serialized as JSON object and return HTTP status code 200
            return Ok(result);
        }

        [HttpPut("{id}")] // /api/vehicles/id
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody]SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Mapping API Resource to domain object
            var vehicle = await _context.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);
            if (vehicle == null)
                return NotFound();
            _mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await _context.SaveChangesAsync();
            var result = _mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            //This will get serialized as JSON object and return HTTP status code 200
            return Ok(result);
        }
        [HttpDelete("{id}")] // /api/vehicle/id
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            //Fetch vehicle with the id
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
                return NotFound();
            _context.Remove(vehicle);
            await _context.SaveChangesAsync();
            return Ok(id);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            //Fetch vehicle with Features with the id
            var vehicle = await _context.Vehicles
                .Include(v => v.Features)
                //EagerLoad Vehicle Features
                .ThenInclude(vf=>vf.Feature)
                .Include(v=>v.Model)
                .ThenInclude(m=>m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
            if (vehicle == null)
                return NotFound();
            var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
    }
}

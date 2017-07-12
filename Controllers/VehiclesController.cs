using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Controllers.Resources;
using ShoppingCart.Core;
using ShoppingCart.Core.Models;

namespace ShoppingCart.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private IMapper _mapper;
        //private ShoppingCartDbContext _context;
        private IVehicleRepository _repository;
        private IUnitOfWork _unitOfWork;

        //So, we have decoupled EF from controller via UOW
        public VehiclesController(IMapper mapper, IVehicleRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
          //  _context = context;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody]SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Custom Error Handling example
            /* var model = await _context.Models.FindAsync(saveVehicleResource.ModelId);
             if (model == null)
             {
                 ModelState.AddModelError("ModelId", "Invalid ModelId");
                 return BadRequest(ModelState);
             }
            */

            //Mapping API Resource to domain object
            var vehicle = _mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource);
            vehicle.LastUpdate = DateTime.Now;
            _repository.Add(vehicle);
            await _unitOfWork.CompleteAsync();

           // await _context.Models.Include(m => m.Make).SingleOrDefaultAsync(m => m.Id == vehicle.ModelId);
            await _repository.GetVehicle(vehicle.Id);

            //Fetch vehicle with Features with the id
            vehicle = await _repository.GetVehicle(vehicle.Id);

            var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);
            //This will get serialized as JSON object and return HTTP status code 200
            return Ok(result);
        }

        [HttpPut("{id}")] // /api/vehicles/id
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody]SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Mapping API Resource to domain object
            var vehicle = await _repository.GetVehicle(id);

            if (vehicle == null)
                return NotFound();
            _mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await _unitOfWork.CompleteAsync();
            vehicle = await _repository.GetVehicle(vehicle.Id);
            var result = _mapper.Map<Vehicle, VehicleResource>(vehicle);
            //This will get serialized as JSON object and return HTTP status code 200
            return Ok(result);
        }
        [HttpDelete("{id}")] // /api/vehicle/id
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            //Fetch vehicle with the id
            var vehicle = await _repository.GetVehicle(id, includeRelated: false);
            if (vehicle == null)
                return NotFound();

            _repository.Remove(vehicle);

            await _unitOfWork.CompleteAsync();
            return Ok(id);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            //Fetch vehicle with Features with the id
            var vehicle = await _repository.GetVehicle(id);
            if (vehicle == null)
                return NotFound();
            var vehicleResource = _mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }
    }
}

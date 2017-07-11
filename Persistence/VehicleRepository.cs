using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Persistence
{

    public class VehicleRepository : IVehicleRepository
    {
        private ShoppingCartDbContext _context;

        public VehicleRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public void Add(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            _context.Remove(vehicle);
        }

        public async Task<Vehicle> GetVehicle(int id, bool includeRelated =true)
        {
            if (includeRelated != false)
                return await _context.Vehicles.FindAsync(id);
            return await _context.Vehicles
                .Include(v => v.Features)
                //EagerLoad Vehicle Features
                .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }
    }
}

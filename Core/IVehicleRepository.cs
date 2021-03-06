﻿using System.Threading.Tasks;
using ShoppingCart.Core.Models;

namespace ShoppingCart.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);   
    }
}

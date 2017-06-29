using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers.Resources
{
    public class ModelResource
    {
        public int Id { get; set; }
      
        public string Name { get; set; }
        
        //Removed relationship explicitly as it was causing loop in json object
    }
}

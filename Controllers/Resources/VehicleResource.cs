using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers.Resources
{
    public class VehicleResource
    {
        public int Id { get; set; }
        public int ModelId { get; set; }

        public bool IsRegistered { get; set; }

        public ContactResource Contact { get; set; }

        public ICollection<int> Features { get; set; }

        //As a best practice, we should always initialize collection prop to avoid null ref
        public VehicleResource()
        {
            Features = new Collection<int>();
        }
    }
}

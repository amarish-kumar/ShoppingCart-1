using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers.Resources
{
    
    public class VehicleResource
    {
        public int Id { get; set; }
        
        public KeyValuePairResource Model { get; set; }
        public KeyValuePairResource Make { get; set; }
        public bool IsRegistered { get; set; }

        //public Contact Contact {get;set;}, currently complex objects are not supported in EF Core.
        //However, this is there in EF 6
      
        public ContactResource Contact { get; set; }
      
       public DateTime LastUpdate { get; set; }

        public ICollection<KeyValuePairResource> Features { get; set; }

        //As a best practice, we should always initialize collection prop to avoid null ref
        public VehicleResource()
        {
            Features = new Collection<KeyValuePairResource>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public bool IsRegistered { get; set; }

        //public Contact Contact {get;set;}, currently complex objects are not supported in EF Core.
        //However, this is there in EF 6
        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }
        [StringLength(255)]
        public string ContactEmail { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }

        public DateTime LastUpdate { get; set; }

        public ICollection<VehicleFeature> Features { get; set; }

        //As a best practice, we should always initialize collection prop to avoid null ref
        public Vehicle()
        {
            Features= new Collection<VehicleFeature>();
        }
    }
}

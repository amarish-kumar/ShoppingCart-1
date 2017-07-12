using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers.Resources
{
    public class MakeResource :KeyValuePairResource
    {
        public ICollection<KeyValuePairResource> Models { get; set; }

        //make.Models= new Collection<Model>(); we don't to repeat this at every place whereever this class is used
        //hence, we should initialize the same ctor level itself.

        public MakeResource()
        {
            Models = new Collection<KeyValuePairResource>();
        }
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class Make
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; }

        //make.Models= new Collection<Model>(); we don't to repeat this at every place whereever this class is used
        //hence, we should initialize the same ctor level itself.

        public Make()
        {
            Models = new Collection<Model>();
        }
    }
}

using AutoMapper;
using ShoppingCart.Controllers.Resources;
using ShoppingCart.Models;

namespace ShoppingCart.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
        }
    }
}

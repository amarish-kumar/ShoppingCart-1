using System.Linq;
using AutoMapper;
using ShoppingCart.Controllers.Resources;
using ShoppingCart.Models;

namespace ShoppingCart.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            //Domain to API Resources
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();

            //API Resources to Domain
            CreateMap<VehicleResource, Vehicle>()
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                //Features property, I mapped it to vehicle resource. But, here we are dealing with 
                //bunch of integers. So, for every new id, we need to create new object
                .ForMember(v => v.Features,
                    opt => opt.MapFrom(vr => vr.Features.Select(id => new VehicleFeature {FeatureId = id})));
        }
    }
}

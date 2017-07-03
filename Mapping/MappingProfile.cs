using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ShoppingCart.Controllers.Resources;
using ShoppingCart.Models;

namespace ShoppingCart.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to API Resources
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(
                    v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
            /*Map is collection of properties, which is getting returned while querying say GetVehicle/1*/
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(
                    v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResource { Id = vf.FeatureId, Name = vf.Feature.Name })));

            //API Resources to Domain
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                //Features property, I mapped it to vehicle resource. But, here we are dealing with 
                //bunch of integers. So, for every new id, we need to create new object
                /* .ForMember(v => v.Features,
                     opt => opt.MapFrom(vr => vr.Features.Select(id => new VehicleFeature {FeatureId = id})));*/
                //.ForMember(v => v.Features, opt => opt.Ignore())
                //.AfterMap((vr, v) =>
                //{
                //    //Remove unselected features
                //    var removedFeatures = new List<VehicleFeature>();
                //    foreach (var f in v.Features)
                //        if (!vr.Features.Contains(f.FeatureId))
                //            //v.Features.Remove(f);, this will throw run time exception as we are iterating and trying to remove from collection
                //            removedFeatures.Add(f);
                //    foreach (var f in removedFeatures)
                //        v.Features.Remove(f);

                //    //Add New Features
                //    foreach (var id in vr.Features)
                //        if (!v.Features.Any(f => f.FeatureId == id))
                //            v.Features.Add(new VehicleFeature {FeatureId = id});
                //});

                //Same above logic using LINQ
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) =>
            {
                //Using select, we can iterate 
                var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId));
                foreach (var f in removedFeatures)
                    v.Features.Remove(f);

                //Add Feature
                var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).
                Select(id => new VehicleFeature { FeatureId = id });
                foreach (var f in addedFeatures)
                    v.Features.Add(f);
            });
        }
    }
}

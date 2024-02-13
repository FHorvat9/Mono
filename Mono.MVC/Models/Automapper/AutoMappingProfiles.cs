using AutoMapper;
using Mono.Service.Models;
using System.Numerics;

namespace Mono.MVC.Models.Automapper
{
    public class AutoMappingProfiles : Profile
    {
        public AutoMappingProfiles()
        {
            CreateMap<VehicleMakeViewModel, VehicleMake>().ReverseMap();


            CreateMap<VehicleModelViewModel, VehicleModel>().ReverseMap();

            //CreateMap<VehicleModel, VehicleModelViewModel>()
            //.ForMember(d => d.Id , d => d.MapFrom(x => x.Id))
            //.ForMember(d => d.Name, d => d.MapFrom(x => x.Name))
            //.ForMember(d => d.Abrv, d => d.MapFrom(x => x.Abrv))
            //.ForMember(d => d.VehicleMakeId, d => d.MapFrom(x => x.vehicleMake.Id));
        }
    }
}

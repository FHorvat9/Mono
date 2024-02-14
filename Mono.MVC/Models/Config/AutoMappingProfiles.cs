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

           
        }
    }
}

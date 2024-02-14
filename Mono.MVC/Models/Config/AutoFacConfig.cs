using Autofac;

using Mono.Service.Repositories;
using Mono.Service.Repositories.Interfaces;

namespace Mono.MVC.Models.Config
{
    public class AutoFacConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VehicleMakeRepository>().As<IVehicleMakeRepository>();
            builder.RegisterType<VehicleModelRepository>().As<IVehicleModelRepository>();
            
        }
    }
}

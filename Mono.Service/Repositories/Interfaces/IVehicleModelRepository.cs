using Mono.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Repositories.Interfaces
{
    public interface IVehicleModelRepository
    {
        IQueryable<VehicleModel> GetAllVehicleModels();
        IQueryable<VehicleModel> GetAllVehicleModels(String sortOrder, String searchString);
        Task<VehicleModel> GetVehicleModelAsync(int id);
        Task<VehicleModel> UpdateVehicleModelAsync(VehicleModel vehicleModelChanged);
        Task<VehicleModel> AddNewVehicleModelAsync(VehicleModel vehicle);
        Task<VehicleModel> DeleteVehicleModelAsync(int id);
       
        
    }
}

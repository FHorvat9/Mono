using Mono.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Repositories.Interfaces
{
    public interface IVehicleMakeRepository
    {

        IQueryable<VehicleMake> GetAllVehicleMakes();
        IQueryable<VehicleMake> GetAllVehicleMakes(String sortOrder, string searchString);
        Task<VehicleMake> GetVehicleMakeAsync(int id);
        Task<VehicleMake> UpdateVehicleMakeAsync(VehicleMake vehicleMakeChanges);
        Task<VehicleMake> AddNewVehicleMakeAsync(VehicleMake vehicle);
        Task<VehicleMake> DeleteVehicleMakeAsync(int id);
    }
}

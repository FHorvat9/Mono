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
        
        IEnumerable<VehicleMake> getAllVehicleMakes();
        IEnumerable<VehicleMake> getAllVehicleMakesFiltered(String sortOrder,String searchString);
        IEnumerable<VehicleMake> getAllVehicleMakesSorted(String sortOrder);

        
        Task<VehicleMake> getVehicle(int id);
        Task<VehicleMake> updateVehicleMake(VehicleMake vehicleMakeChanges);
        Task<VehicleMake> addNew(VehicleMake vehicle);
        Task<VehicleMake> delete(int id);
    }
}

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
        IEnumerable<VehicleModel> GetAllVehicleModels();
        Task<VehicleModel> GetModel(int id);
        Task<VehicleModel> UpdateVehicleModel(VehicleModel vehicleModelChanged);
        Task<VehicleModel> AddNew(VehicleModel vehicle);
        Task<VehicleModel> Delete(int id);
        IEnumerable<VehicleModel> getAllVehicleModelsFiltered(String sortOrder, String searchString);
        IEnumerable<VehicleModel> getAllVehicleModelsSorted(String sortOrder);
    }
}

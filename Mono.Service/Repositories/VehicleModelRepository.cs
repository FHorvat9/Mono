
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mono.Service.Models;
using Mono.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Repositories
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        VehicleDbContext _context;

        public VehicleModelRepository(VehicleDbContext context)
        {

            _context = context;
        }
        public IQueryable<VehicleModel> GetAllVehicleModels()
        {


            return _context.VehicleModels.Include(e => e.vehicleMake);

        }
        public IQueryable<VehicleModel> GetAllVehicleModels(String sortOrder, String searchString)
        {
            if (sortOrder == null)
            {
                sortOrder = "";
            }

            switch (sortOrder)
            {
                case "name_desc":
                    return getAllVehicleModelsFiltered(searchString).OrderByDescending(e => e.Name);
                case "abrv_desc":
                    return getAllVehicleModelsFiltered(searchString).OrderByDescending(e => e.Abrv);
                case "make_desc":
                    return getAllVehicleModelsFiltered(searchString).OrderByDescending(e => e.vehicleMake.Name);
                case "abrv":
                    return getAllVehicleModelsFiltered(searchString).OrderBy(e => e.Abrv);
                    
                case "make":
                    return getAllVehicleModelsFiltered(searchString).OrderBy(e => e.vehicleMake.Name);
                    
                

                default:

                    return getAllVehicleModelsFiltered(searchString).OrderBy(e => e.Name);

            }
        }
        private IQueryable<VehicleModel> getAllVehicleModelsFiltered(String searchString)
        {
            if (!searchString.IsNullOrEmpty())
            {
                return _context.VehicleModels.Include(e => e.vehicleMake).Where(e => e.Name.Contains(searchString) || e.Abrv.Contains(searchString));
            }
            return _context.VehicleModels.Include(e => e.vehicleMake);
        }

        public async Task<VehicleModel> AddNewVehicleModelAsync(VehicleModel vehicle)
        {
            _context.Add(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        public async Task<VehicleModel> DeleteVehicleModelAsync(int id)
        {

            _context.VehicleModels.Remove(await _context.VehicleModels.FindAsync(id));
            await _context.SaveChangesAsync();



            return null;
        }

       

        public async Task<VehicleModel> GetVehicleModelAsync(int id)
        {


            return await _context.VehicleModels.Include(e => e.vehicleMake).Where(e => e.Id == id).FirstOrDefaultAsync();

        }

        public async Task<VehicleModel> UpdateVehicleModelAsync(VehicleModel vehicleModelChanged)
        {
            var oldVehicle = await GetVehicleModelAsync(vehicleModelChanged.Id);

            if (vehicleModelChanged != null)
            {
                
                oldVehicle.Abrv = vehicleModelChanged.Abrv;
                oldVehicle.Name = vehicleModelChanged.Name;
                oldVehicle.VehicleMakeId = vehicleModelChanged.VehicleMakeId;


                await _context.SaveChangesAsync();
            }
            return null;
        }
    }
}


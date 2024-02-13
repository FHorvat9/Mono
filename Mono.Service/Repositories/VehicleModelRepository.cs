
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
        public IEnumerable<VehicleModel> getAllVehicleModelsFiltered(String sortOrder, String searchString)
        {
            if (sortOrder == null)
            {
                sortOrder = "";
            }

            switch (sortOrder)
            {
                case "name_desc":
                    return _context.VehicleModels.OrderByDescending(e => e.Name).Where(e => e.Name.Contains(searchString) || e.Abrv.Contains(searchString)).Include(e => e.vehicleMake);
                   
                case "abrv_desc":
                    return _context.VehicleModels.OrderByDescending(e => e.Abrv).Where(e => e.Name.Contains(searchString) || e.Abrv.Contains(searchString)).Include(e => e.vehicleMake);
                   
                case "abrv":
                    return _context.VehicleModels.OrderBy(e => e.Abrv).Where(e => e.Name.Contains(searchString) || e.Abrv.Contains(searchString)).Include(e => e.vehicleMake);
                    
                case "make":
                    return _context.VehicleModels.OrderBy(e => e.vehicleMake.Name).Where(e => e.vehicleMake.Name.Contains(searchString)).Include(e => e.vehicleMake);
                    
                case "make_desc":
                    return _context.VehicleModels.OrderBy(e => e.vehicleMake.Name).Where(e => e.vehicleMake.Name.Contains(searchString)).Include(e => e.vehicleMake);
                   

                default:

                    return _context.VehicleModels.OrderBy(e => e.Name).Where(e => e.Name.Contains(searchString) || e.Abrv.Contains(searchString)).Include(e => e.vehicleMake);

            }
        }
        public IEnumerable<VehicleModel> getAllVehicleModelsSorted(String sortOrder)
        {
            if (sortOrder == null)
            {
                sortOrder = "";
            }

            switch (sortOrder)
            {
                case "name_desc":
                    return _context.VehicleModels.OrderByDescending(e => e.Name).Include(e => e.vehicleMake);
                   
                case "abrv_desc":
                    return _context.VehicleModels.OrderByDescending(e => e.Abrv).Include(e => e.vehicleMake);
                    
                case "abrv":
                    return _context.VehicleModels.OrderBy(e => e.Abrv).Include(e => e.vehicleMake);
                   
                case "make":
                    return _context.VehicleModels.OrderBy(e => e.vehicleMake.Name).Include(e => e.vehicleMake);
                   
                case "make_desc":
                    return _context.VehicleModels.OrderByDescending(e => e.vehicleMake.Name).Include(e => e.vehicleMake);
                   

                default:

                    return _context.VehicleModels.OrderBy(e=>e.Name).Include(e => e.vehicleMake);

            }
        }

        public async Task<VehicleModel> AddNew(VehicleModel vehicle)
        {
            _context.Add(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        public async Task<VehicleModel> Delete(int id)
        {

            _context.VehicleModels.Remove(await _context.VehicleModels.FindAsync(id));
            await _context.SaveChangesAsync();



            return null;
        }

        public IEnumerable<VehicleModel> GetAllVehicleModels()
        {


            return _context.VehicleModels.Include(e => e.vehicleMake);

        }

        public async Task<VehicleModel> GetModel(int id)
        {


            return await _context.VehicleModels.Include(e => e.vehicleMake).Where(e => e.Id == id).FirstOrDefaultAsync();

        }

        public async Task<VehicleModel> UpdateVehicleModel(VehicleModel vehicleModelChanged)
        {
            var oldVehicle = await GetModel(vehicleModelChanged.Id);

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


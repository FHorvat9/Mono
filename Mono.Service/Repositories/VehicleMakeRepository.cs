using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mono.Service.Models;
using Mono.Service.Repositories.Interfaces;



namespace Mono.Service.Repositories
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {

        VehicleDbContext _context;

        public VehicleMakeRepository(VehicleDbContext context)
        {

            _context = context;
        }

        public async Task<VehicleMake> AddNewVehicleMakeAsync(VehicleMake vehicle)
        {
            _context.Add(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        public async Task<VehicleMake> DeleteVehicleMakeAsync(int id)
        {
            VehicleMake vehicle = await _context.VehicleMakes.FindAsync(id);
            if (vehicle.VehicleModels.IsNullOrEmpty())
            {

                _context.VehicleMakes.Remove(vehicle);
                await _context.SaveChangesAsync();

            }

            return vehicle;
        }

        public IQueryable<VehicleMake> GetAllVehicleMakes()
        {
            return _context.VehicleMakes;
       }

        private IQueryable<VehicleMake> GetAllVehicleMakesFiltered(String searchString)
        {
            if (!searchString.IsNullOrEmpty())
            {
                return  _context.VehicleMakes.Where(e => e.Name.Contains(searchString) || e.Abrv.Contains(searchString));
            }
            return _context.VehicleMakes;


        }

        public IQueryable<VehicleMake> GetAllVehicleMakes(String sortOrder, String searchString)
        {
            if (sortOrder == null)
            {
                sortOrder = "";
            }

            switch (sortOrder)
            {
                case "name_desc":
                    return GetAllVehicleMakesFiltered(searchString).OrderByDescending(e => e.Name);

                case "abrv_desc":
                    return GetAllVehicleMakesFiltered(searchString).OrderByDescending(e => e.Abrv);

                case "abrv":
                    return GetAllVehicleMakesFiltered(searchString).OrderBy(e => e.Abrv);
                default:

                    return GetAllVehicleMakesFiltered(searchString).OrderBy(e => e.Name);

            }
        }




        public async Task<VehicleMake> GetVehicleMakeAsync(int id)
        {
            return await _context.VehicleMakes.FindAsync(id);
        }

        public async Task<VehicleMake> UpdateVehicleMakeAsync(VehicleMake vehicleMakeChanges)
        {
            var oldVehicle = await GetVehicleMakeAsync(vehicleMakeChanges.Id);


            if (oldVehicle != null)
            {
                oldVehicle.Name = vehicleMakeChanges.Name;
                oldVehicle.Abrv = vehicleMakeChanges.Abrv;
                await _context.SaveChangesAsync();
            }


            return null;
        }
    }
}

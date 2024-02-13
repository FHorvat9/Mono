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

        public async Task<VehicleMake> addNew(VehicleMake vehicle)
        {
            _context.Add(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        public async Task<VehicleMake> delete(int id)
        {
            VehicleMake vehicle = await _context.VehicleMakes.FindAsync(id);
            if (vehicle.VehicleModels.IsNullOrEmpty())
            {

                _context.VehicleMakes.Remove(vehicle);
                await _context.SaveChangesAsync();

            }

            return vehicle;
        }

        public IEnumerable<VehicleMake> getAllVehicleMakes()
        {
            return _context.VehicleMakes;
        }

        public IEnumerable<VehicleMake> getAllVehicleMakesFiltered(String sortOrder, String searchString)
        {
            if (sortOrder == null)
            {
                sortOrder = "";
            }

            switch (sortOrder)
            {
                case "name_desc":
                    return _context.VehicleMakes.OrderByDescending(e => e.Name).Where(e => e.Name.Contains(searchString) || e.Abrv.Contains(searchString));

                case "abrv_desc":
                    return _context.VehicleMakes.OrderByDescending(e => e.Abrv).Where(e => e.Name.Contains(searchString) || e.Abrv.Contains(searchString));

                case "abrv":
                    return _context.VehicleMakes.OrderBy(e => e.Abrv).Where(e => e.Name.Contains(searchString) || e.Abrv.Contains(searchString));


                default:

                    return _context.VehicleMakes.Where(e => e.Name.Contains(searchString) || e.Abrv.Contains(searchString));

            }
        }
        public IEnumerable<VehicleMake> getAllVehicleMakesSorted(String sortOrder)
        {


            switch (sortOrder)
            {
                case "name_desc":
                    return _context.VehicleMakes.OrderByDescending(e => e.Name);

                case "abrv_desc":
                    return _context.VehicleMakes.OrderByDescending(e => e.Abrv);

                case "abrv":
                    return _context.VehicleMakes.OrderBy(e => e.Abrv);


                default:

                    return _context.VehicleMakes;

            }
        }




        public async Task<VehicleMake> getVehicle(int id)
        {
            return await _context.VehicleMakes.FindAsync(id);
        }

        public async Task<VehicleMake> updateVehicleMake(VehicleMake vehicleMakeChanges)
        {
            var oldVehicle = await getVehicle(vehicleMakeChanges.Id);


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

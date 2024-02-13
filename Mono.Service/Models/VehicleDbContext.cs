using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Mono.Service.Models
{

    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions options) : base(options){}
        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
    }

   



}

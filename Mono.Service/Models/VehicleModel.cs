using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Models
{
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Abrv { get; set; }

        public int VehicleMakeId { get; set;}
        public VehicleMake vehicleMake { get; set; }

    }
}

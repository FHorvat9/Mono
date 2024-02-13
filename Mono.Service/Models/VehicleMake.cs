using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mono.Service.Models
{
    public class VehicleMake
    {
        
        public int Id { get; set; }
        public String Name { get; set; }
        public String Abrv { get; set; }

        public ICollection<VehicleModel> VehicleModels { get; set; }
    }
}

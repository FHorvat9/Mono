using Mono.Service.Models;
using System.ComponentModel.DataAnnotations;

namespace Mono.MVC.Models
{
    public class VehicleMakeViewModel
    {
        public int Id { get; set; }
        
        public String Name { get; set; }
        
        public String Abrv { get; set; }

        public ICollection<VehicleModel> VehicleModels { get; set; }
    }
}

using Mono.Service.Models;

namespace Mono.MVC.Models
{
    public class VehicleModelViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public int VehicleMakeId { get; set; }
        
        public VehicleMake VehicleMake { get; set; }

    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Mono.MVC.Models;
using Mono.Service.Models;
using Mono.Service.Repositories.Interfaces;
using System.Configuration;

namespace Mono.MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly IVehicleModelRepository _ModelRepo;
        private readonly IVehicleMakeRepository _MakeRepo;
        private readonly IMapper _Mapper;

        public VehicleModelController(IVehicleMakeRepository _vehicleMakeRepository, IVehicleModelRepository _vehicleModelRepository, IMapper mapping)
        {
            _ModelRepo = _vehicleModelRepository;
            _Mapper = mapping;
            _MakeRepo = _vehicleMakeRepository;
        }

        public async Task<IActionResult> Index(String sortOrder, String currentFilter, String searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = sortOrder == "abrv" ? "abrv_desc" : "abrv";
            ViewData["MakeSortParm"] = sortOrder == "make" ? "make_desc" : "make";
            

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var vehicles = searchString.IsNullOrEmpty() ? _ModelRepo.getAllVehicleModelsSorted(sortOrder) : _ModelRepo.getAllVehicleModelsFiltered(sortOrder, searchString);
            int pageSize = 5;


            return View(await PaginatedList<VehicleModel>.createPaginatedList(vehicles.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            SelectList makeList =  new SelectList( _MakeRepo.getAllVehicleMakes(),"Id","Name");
            ViewBag.makeList = makeList;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleModelViewModel vehicleModelViewModel)
        {
            var vehicleModel = _Mapper.Map<VehicleModel>(vehicleModelViewModel);          
            await _ModelRepo.AddNew(vehicleModel);
            return RedirectToAction("Index", "VehicleModel");

           
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicleModel = await _ModelRepo.GetModel(id);
            var vehicleModelViewModel = _Mapper.Map<VehicleModelViewModel>(vehicleModel);


            
            return View(vehicleModelViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(VehicleModelViewModel viewModel)
        {
            await _ModelRepo.Delete(viewModel.Id);



            return RedirectToAction("index","VehicleModel");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vehicleModel = await _ModelRepo.GetModel(id);
            SelectList makeList = new SelectList(_MakeRepo.getAllVehicleMakes(), "Id", "Name");
            ViewBag.makeList = makeList;
            var vehicleModelViewModel = _Mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View(vehicleModelViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(VehicleModelViewModel vehicleModelViewModel)
        {
            var vehicleModel = _Mapper.Map<VehicleModel>(vehicleModelViewModel);
            await _ModelRepo.UpdateVehicleModel(vehicleModel);

            return RedirectToAction("index", "VehicleModel");

        }



    }
}

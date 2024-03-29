﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mono.MVC.Models;
using Mono.Service.Models;
using Mono.Service.Repositories;
using Mono.Service.Repositories.Interfaces;

namespace Mono.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
       private readonly IVehicleMakeRepository _MakeRepo;
       private readonly IMapper _Mapper;


        public VehicleMakeController(IVehicleMakeRepository _vehicleMakeRepository,IMapper mapping)
        {
           _MakeRepo = _vehicleMakeRepository;
           _Mapper = mapping;
        }

        
           
        public async Task<IActionResult> Index(String sortOrder, String currentFilter, String searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = sortOrder == "abrv" ? "abrv_desc" : "abrv";
            

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var vehicles =  _MakeRepo.GetAllVehicleMakes(sortOrder, searchString);
            
            int pageSize = 5;

            
            return View(await PaginatedList<VehicleMake>.createPaginatedList(vehicles, pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            
            return View();

        }


        [HttpPost]
        public async Task<IActionResult> Create(VehicleMakeViewModel vehicleMakeViewModel)
        {
            var vehicleMake = _Mapper.Map<VehicleMake>(vehicleMakeViewModel);
            await _MakeRepo.AddNewVehicleMakeAsync(vehicleMake);
            return RedirectToAction("Index", "VehicleMake");

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vehicleMake = await _MakeRepo.GetVehicleMakeAsync(id);
            var vehicleMakeViewModel = _Mapper.Map<VehicleMakeViewModel>(vehicleMake);
            

            return View(vehicleMakeViewModel);

        }


        [HttpPost]
        public async Task<IActionResult> Edit(VehicleMakeViewModel viewMake)
        {
            var vehicleMake = _Mapper.Map<VehicleMake>(viewMake);
           await _MakeRepo.UpdateVehicleMakeAsync(vehicleMake);

            


            return RedirectToAction("Index","VehicleMake");

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicleMake = await _MakeRepo.GetVehicleMakeAsync(id);
            var vehicleMakeViewModel = _Mapper.Map<VehicleMakeViewModel>(vehicleMake);
            if (!vehicleMakeViewModel.VehicleModels.IsNullOrEmpty()) {
                return null;
            }

            return View(vehicleMakeViewModel);

        }


        [HttpPost]
        public async Task<IActionResult> Delete(VehicleMakeViewModel viewMake)
        {
            await _MakeRepo.DeleteVehicleMakeAsync(viewMake.Id);




            return RedirectToAction("Index", "VehicleMake");

        }
    }
}

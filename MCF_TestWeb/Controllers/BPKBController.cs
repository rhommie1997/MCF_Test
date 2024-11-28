using MCF_TestWeb.Models;
using MCF_TestWeb.Services.Interface;
using MCF_TestWeb.Services.Services;
using MCF_TestWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MCF_TestWeb.Controllers
{
    public class BPKBController : Controller
    {
        private readonly IBPKBService bpkbService;
        public BPKBController(IBPKBService bpkbService)
        {
            this.bpkbService = bpkbService;
        }
        public async Task<IActionResult> Index()
        {
            List<BpkbViewModel>? listBpkb = new();
            ResponseDto? response = await bpkbService.GetAllBpkbAsync();
            if (response != null && response.IsSuccess)
            {
                listBpkb = JsonConvert.DeserializeObject<List<BpkbViewModel>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["Error"] = response.Message;
            }

            return View(listBpkb);
        }
        public async Task<IActionResult> Add()
        {
            var newOne = new BpkbViewModel();

            var locations = await bpkbService.GetAllLocations();

            if (locations != null && locations.IsSuccess)
            {
                newOne.locationList = JsonConvert.DeserializeObject<List<LocationViewModel>>(Convert.ToString(locations.Result)).ToList();

            }
            return View("_Add", newOne);
        }

        public async Task<IActionResult> Save(string Type, BpkbAddViewModel data)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? res = new ResponseDto();

                if (Type == "Add")
                {
                    res = await bpkbService.CreateBpkbAsync(data);
                }
                else
                {
                    res = await bpkbService.UpdateBpkbAsync(data);
                }

                if (res != null && res.IsSuccess)
                {
                    if (Type == "Add")
                    {
                        TempData["Success"] = "Bpkb successfully created";
                    }
                    else
                    {
                        TempData["Success"] = "Bpkb successfully updated";
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = res.Message;
                }
            }

            return View(data);
        }

        public async Task<IActionResult> Update(string id)
        {
            ResponseDto? res = await bpkbService.GetBpkbByID(id);
            if (res != null && res.IsSuccess)
            {
                BpkbViewModel bpkb = JsonConvert.DeserializeObject<BpkbViewModel>(Convert.ToString(res.Result));
                var locations = await bpkbService.GetAllLocations();

                if (locations != null && locations.IsSuccess)
                {
                    bpkb.locationList = JsonConvert.DeserializeObject<List<LocationViewModel>>(Convert.ToString(locations.Result)).ToList();
                }

                return View("_Edit", bpkb);
            }
            else
            {
                TempData["Error"] = res.Message;
            }

            return NotFound();
        }

        public async Task<IActionResult> Delete(string id)
        {
            ResponseDto? res = await bpkbService.DeleteBpkb(id);
            if (res != null && res.IsSuccess)
            {
                TempData["Success"] = "Bpkb successfully deleted";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = res.Message;
            }


            return NotFound();
        }
    }
}

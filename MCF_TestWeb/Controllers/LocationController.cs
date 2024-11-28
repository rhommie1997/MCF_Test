using MCF_TestWeb.Models;
using MCF_TestWeb.Services.Interface;
using MCF_TestWeb.Services.Services;
using MCF_TestWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MCF_TestWeb.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationService locService;
        public LocationController(ILocationService locService)
        {
            this.locService = locService;
        }
        public async Task<IActionResult> Index()
        {
            List<LocationViewModel>? listBpkb = new();
            ResponseDto? response = await locService.GetAllLocAsync();
            if (response != null && response.IsSuccess)
            {
                listBpkb = JsonConvert.DeserializeObject<List<LocationViewModel>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["Error"] = response.Message;
            }

            return View(listBpkb);
        }

        public async Task<IActionResult> Add()
        {
            return View("_Add");
        }

        public async Task<IActionResult> Save(string Type, LocationViewModel data)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? res = new ResponseDto();

                if (Type == "Add")
                {
                    res = await locService.CreateLocAsync(data);
                }
                else
                {
                    res = await locService.UpdateLocAsync(data);
                }

                if (res != null && res.IsSuccess)
                {
                    if (Type == "Add")
                    {
                        TempData["Success"] = "Location successfully created";
                    }
                    else
                    {
                        TempData["Success"] = "Location successfully updated";
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
            ResponseDto? res = await locService.GetLocByID(id);
            if (res != null && res.IsSuccess)
            {
                LocationViewModel location = JsonConvert.DeserializeObject<LocationViewModel>(Convert.ToString(res.Result));

                return View("_Edit", location);
            }
            else
            {
                TempData["Error"] = res.Message;
            }

            return NotFound();
        }

        public async Task<IActionResult> Delete(string id)
        {
            ResponseDto? res = await locService.DeleteLoc(id);
            if (res != null && res.IsSuccess)
            {
                TempData["Success"] = "Location successfully deleted";
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

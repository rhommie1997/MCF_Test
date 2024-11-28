using AutoMapper;
using MCF_TestAPI.Data;
using MCF_TestAPI.Models;
using MCF_TestAPI.Models.Dto;
using MCF_TestAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCF_TestAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly MCF_TestAPI_DbContext _db;
        private ResponseDto _res;
        private IMapper _mapper;

        public LocationController(MCF_TestAPI_DbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _res = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                var objList = _db.ms_storage_locations.ToList();
                _res.Result = _mapper.Map<List<LocationViewModel>>(objList);
            }
            catch (Exception e)
            {
                _res.IsSuccess = false;
                _res.Message = e.Message;
            }

            return _res;
        }

        [HttpGet]
        [Route("{location_id}")]
        public ResponseDto Get(string location_id)
        {
            try
            {
                var obj = _db.ms_storage_locations.FirstOrDefault(x => x.location_id == location_id);
                _res.Result = _mapper.Map<LocationViewModel>(obj);
            }
            catch (Exception e)
            {
                _res.IsSuccess = false;
                _res.Message = e.Message;
            }
            return _res;
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public ResponseDto GetByName(string name)
        {
            try
            {
                var objList = _db.ms_storage_locations.Where(x => x.location_name.Contains(name)).ToList();
                _res.Result = _mapper.Map<List<LocationViewModel>>(objList);
            }
            catch (Exception e)
            {
                _res.IsSuccess = false;
                _res.Message = e.Message;
            }

            return _res;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] LocationViewModel bvm)
        {
            try
            {
                ms_storage_location loc = _mapper.Map<ms_storage_location>(bvm);
                _db.ms_storage_locations.Add(loc);
                _db.SaveChanges();

                _res.Result = _mapper.Map<LocationViewModel>(loc);
            }
            catch (Exception e)
            {
                _res.IsSuccess = false;
                _res.Message = e.Message;
            }

            return _res;
        }

        [HttpPut]
        public ResponseDto Update([FromBody] LocationViewModel bvm)
        {
            try
            {
                ms_storage_location loc = _db.ms_storage_locations.Where(x => x.location_id == bvm.location_id).FirstOrDefault();
                if (loc != null)
                {
                    loc.location_id = bvm.location_id;
                    loc.location_name = bvm.location_name;
                    _db.ms_storage_locations.Update(loc);
                    _db.SaveChanges();
                }
                else
                {
                    _res.IsSuccess = false;
                    _res.Message = "Data Not Found!";
                }
                _res.Result = _mapper.Map<LocationViewModel>(loc);
            }
            catch (Exception e)
            {
                _res.IsSuccess = false;
                _res.Message = e.Message;
            }
            return _res;
        }

        [HttpDelete]
        public ResponseDto Delete(string location_id)
        {
            try
            {
                ms_storage_location loc = _db.ms_storage_locations.FirstOrDefault(x => x.location_id == location_id);
                if (loc != null)
                {
                    _db.ms_storage_locations.Remove(loc);
                    _db.SaveChanges();
                }
                else
                {
                    _res.IsSuccess = false;
                    _res.Message = "Data Not Found!";
                }
                _res.Result = _mapper.Map<LocationViewModel>(loc);
            }
            catch (Exception e)
            {
                _res.IsSuccess = false;
                _res.Message = e.Message;
            }
            return _res;
        }

    }
}

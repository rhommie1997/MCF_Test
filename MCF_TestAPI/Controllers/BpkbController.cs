using AutoMapper;
using MCF_TestAPI.Data;
using MCF_TestAPI.Models;
using MCF_TestAPI.Models.Dto;
using MCF_TestAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MCF_TestAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BpkbController : ControllerBase
    {

        private readonly MCF_TestAPI_DbContext _db;
        private ResponseDto _res;
        private IMapper _mapper;

        public BpkbController(MCF_TestAPI_DbContext db, IMapper mapper)
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
                var objList = _db.tr_bpkbs.Select(x => new BpkbViewModel()
                {
                    agreement_number = x.agreement_number,
                    bpkb_date = x.bpkb_date,
                    bpkb_date_in = x.bpkb_date_in,
                    bpkb_no = x.bpkb_no,
                    branch_id = x.branch_id,
                    created_by = x.created_by,
                    created_on = x.created_on,
                    faktur_date = x.faktur_date,
                    faktur_no = x.faktur_no,
                    last_updated_by = x.last_updated_by,
                    last_updated_on = x.last_updated_on,
                    location_id = x.location_id,
                    location_name = x.Location.location_name,
                    police_no = x.police_no
                }).ToList();
                _res.Result = objList;
            }
            catch (Exception e)
            {
                _res.IsSuccess = false;
                _res.Message = e.Message;
            }

            return _res;
        }

        [HttpGet]
        [Route("{id}")]
        public ResponseDto Get(string id)
        {
            try
            {
                var obj = _db.tr_bpkbs.FirstOrDefault(x => x.agreement_number == id);
                _res.Result = _mapper.Map<BpkbViewModel>(obj);
            }
            catch (Exception e)
            {
                _res.IsSuccess = false;
                _res.Message = e.Message;
            }

            return _res;
        }

        [HttpGet]
        [Route("GetByName/{bpkb_no}")]
        public ResponseDto GetByName(string bpkb_no)
        {
            try
            {
                var objList = _db.tr_bpkbs.Where(x => x.bpkb_no.Contains(bpkb_no)).ToList();
                _res.Result = _mapper.Map<List<BpkbViewModel>>(objList);
            }
            catch (Exception e)
            {
                _res.IsSuccess = false;
                _res.Message = e.Message;
            }

            return _res;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] BpkbViewModel pvm)
        {
            try
            {
                tr_bpkb bpkb = _mapper.Map<tr_bpkb>(pvm);
                bpkb.created_by = "System";
                bpkb.created_on = DateTime.Now;
                bpkb.last_updated_by = "System";
                bpkb.last_updated_on = DateTime.Now;
                _db.tr_bpkbs.Add(bpkb);
                _db.SaveChanges();

                _res.Result = _mapper.Map<BpkbViewModel>(bpkb);
            }
            catch (Exception e)
            {
                _res.IsSuccess = false;
                _res.Message = e.Message;
            }

            return _res;
        }

        [HttpPut]
        public ResponseDto Update([FromBody] BpkbViewModel pvm)
        {
            try
            {
                tr_bpkb bpkb = _db.tr_bpkbs.Where(x => x.agreement_number == pvm.agreement_number).FirstOrDefault();
                if (bpkb != null)
                {
                    bpkb.branch_id = pvm.branch_id;
                    bpkb.bpkb_no = pvm.bpkb_no;
                    bpkb.bpkb_date_in = pvm.bpkb_date_in;
                    bpkb.bpkb_date = pvm.bpkb_date;
                    bpkb.faktur_no = pvm.faktur_no;
                    bpkb.faktur_date = pvm.faktur_date;
                    bpkb.police_no = pvm.police_no;
                    bpkb.location_id = pvm.location_id;
                    _db.tr_bpkbs.Update(bpkb);
                    _db.SaveChanges();
                }
                else
                {
                    _res.IsSuccess = false;
                    _res.Message = "Data Not Found!";
                }
                _res.Result = _mapper.Map<BpkbViewModel>(bpkb);
            }
            catch (Exception e)
            {
                _res.IsSuccess = false;
                _res.Message = e.Message;
            }
            return _res;
        }
        [HttpDelete]
        public ResponseDto Delete(string agreement_number)
        {
            try
            {
                tr_bpkb bpkb = _db.tr_bpkbs.FirstOrDefault(x => x.agreement_number == agreement_number);
                if (bpkb != null)
                {
                    ;
                    _db.tr_bpkbs.Remove(bpkb);
                    _db.SaveChanges();
                }
                else
                {
                    _res.IsSuccess = false;
                    _res.Message = "Data Not Found!";
                }
                _res.Result = _mapper.Map<BpkbViewModel>(bpkb);
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

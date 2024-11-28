using MCF_TestWeb.Models;
using MCF_TestWeb.Services.Interface;
using Newtonsoft.Json;
using static MCF_TestWeb.Util.SD;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using MCF_TestWeb.Util;
using Microsoft.AspNetCore.Http;
using MCF_TestWeb.ViewModels;

namespace MCF_TestWeb.Services.Services
{
    public class LocationService : ILocationService
    {
        private readonly IBaseService bs;
        private readonly HttpContext httpContext;
        public LocationService(IBaseService bs, IHttpContextAccessor httpContextAccessor)
        {
            this.bs = bs;
            httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor.HttpContext));
        }

        public async Task<ResponseDto?> CreateLocAsync(LocationViewModel data)
        {
            return await bs.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = data,
                Url = SD.MCF_TestAPIBase + "/api/Location",
                AccessToken = httpContext.User.Claims.FirstOrDefault(x => x.Type == "token")?.Value ?? ""
            });
        }

        public async Task<ResponseDto?> DeleteLoc(string id)
        {
            return await bs.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.MCF_TestAPIBase + "/api/Location?location_id=" + id,
                AccessToken = httpContext.User.Claims.FirstOrDefault(x => x.Type == "token")?.Value ?? ""
            });
        }

        public async Task<ResponseDto?> GetAllLocAsync()
        {
            return await bs.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.MCF_TestAPIBase + "/api/Location",
                AccessToken = httpContext.User.Claims.FirstOrDefault(x => x.Type == "token")?.Value ?? ""
            });
        }

        public async Task<ResponseDto?> GetLocByID(string id)
        {
            return await bs.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.MCF_TestAPIBase + "/api/Location/" + id,
                AccessToken = httpContext.User.Claims.FirstOrDefault(x => x.Type == "token")?.Value ?? ""
            });
        }

        public async Task<ResponseDto?> UpdateLocAsync(LocationViewModel data)
        {
            return await bs.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = data,
                Url = SD.MCF_TestAPIBase + "/api/Location",
                AccessToken = httpContext.User.Claims.FirstOrDefault(x => x.Type == "token")?.Value ?? ""
            });
        }
    }
}

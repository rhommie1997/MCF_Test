using MCF_TestWeb.Models;
using MCF_TestWeb.Services.Interface;
using MCF_TestWeb.Util;
using MCF_TestWeb.ViewModels;
using Microsoft.AspNetCore.Http;

namespace MCF_TestWeb.Services.Services
{
    public class BPKBService : IBPKBService
    {
        private readonly IBaseService bs;
        private readonly HttpContext httpContext;
        public BPKBService(IBaseService bs, IHttpContextAccessor httpContextAccessor)
        {
            this.bs = bs;
            httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor.HttpContext));
        }

        public async Task<ResponseDto?> CreateBpkbAsync(BpkbAddViewModel data)
        {
            return await bs.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = data,
                Url = SD.MCF_TestAPIBase + "/api/Bpkb",
                AccessToken = httpContext.User.Claims.FirstOrDefault(x => x.Type == "token")?.Value ?? ""
            });
        }

        public async Task<ResponseDto?> DeleteBpkb(string id)
        {
            return await bs.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.MCF_TestAPIBase + "/api/Bpkb?agreement_number=" + id,
                AccessToken = httpContext.User.Claims.FirstOrDefault(x => x.Type == "token")?.Value ?? ""
            });
        }

        public async Task<ResponseDto?> GetAllBpkbAsync()
        {
            return await bs.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.MCF_TestAPIBase + "/api/Bpkb",
                AccessToken = httpContext.User.Claims.FirstOrDefault(x => x.Type == "token")?.Value ?? ""
            });
        }

        public async Task<ResponseDto?> GetAllLocations()
        {
            return await bs.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.MCF_TestAPIBase + "/api/Location",
                AccessToken = httpContext.User.Claims.FirstOrDefault(x => x.Type == "token")?.Value ?? ""
            });
        }

        public async Task<ResponseDto?> GetBpkbByID(string id)
        {
            return await bs.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.MCF_TestAPIBase + "/api/Bpkb/" + id,
                AccessToken = httpContext.User.Claims.FirstOrDefault(x => x.Type == "token")?.Value ?? ""
            });
        }

        public async Task<ResponseDto?> UpdateBpkbAsync(BpkbAddViewModel data)
        {
            return await bs.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = data,
                Url = SD.MCF_TestAPIBase + "/api/Bpkb",
                AccessToken = httpContext.User.Claims.FirstOrDefault(x => x.Type == "token")?.Value ?? ""
            });
        }
    }
}

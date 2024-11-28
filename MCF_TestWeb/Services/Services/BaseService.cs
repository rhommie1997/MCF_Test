using MCF_TestWeb.Models;
using MCF_TestWeb.Services.Interface;
using Newtonsoft.Json;
using static MCF_TestWeb.Util.SD;
using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace MCF_TestWeb.Services.Services
{
    public class BaseService : IBaseService
    {

        private readonly IHttpClientFactory httpClientFactory;
        public BaseService(IHttpClientFactory _hcf)
        {
            httpClientFactory = _hcf;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto reqDto)
        {
            try
            {
                HttpClient client = httpClientFactory.CreateClient("MCF_TestAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                if (!string.IsNullOrEmpty(reqDto.AccessToken))
                {
                    message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", reqDto.AccessToken);
                }
                message.RequestUri = new Uri(reqDto.Url);
                if (reqDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(reqDto.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? ap = null;

                switch (reqDto.ApiType)
                {
                    case ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Post;
                        break;
                }

                ap = await client.SendAsync(message);

                switch (ap.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access is denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var ac = await ap.Content.ReadAsStringAsync();
                        var ard = JsonConvert.DeserializeObject<ResponseDto>(ac);
                        return ard;
                }
            }
            catch (Exception e)
            {
                var dto = new ResponseDto()
                {
                    Message = e.Message,
                    IsSuccess = false
                };
                return dto;
            }
        }
    }
}

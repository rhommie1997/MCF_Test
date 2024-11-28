using MCF_TestWeb.Util;
using System.Security.AccessControl;
using static MCF_TestWeb.Util.SD;

namespace MCF_TestWeb.Models
{
    public class RequestDto
    {
        public RequestDto()
        {
            ApiType = ApiType.GET;
        }
        public ApiType ApiType { get; set; }
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}

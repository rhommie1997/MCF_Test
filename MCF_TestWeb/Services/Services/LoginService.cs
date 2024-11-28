using MCF_TestWeb.Models;
using MCF_TestWeb.Services.Interface;
using MCF_TestWeb.Util;
using MCF_TestWeb.ViewModels;

namespace MCF_TestWeb.Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly IBaseService bs;
        public LoginService(IBaseService bs)
        {
            this.bs = bs;
        }

        public async Task<ResponseDto?> GetResult(UserLoginViewModel user)
        {
            return await bs.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = user,
                Url = SD.MCF_TestAPIBase + "/api/Login"
            });
        }
    }
}

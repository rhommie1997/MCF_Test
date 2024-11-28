using MCF_TestWeb.Models;
using MCF_TestWeb.ViewModels;

namespace MCF_TestWeb.Services.Interface
{
    public interface ILoginService
    {
        Task<ResponseDto?> GetResult(UserLoginViewModel user);
    }
}

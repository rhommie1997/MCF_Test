using MCF_TestWeb.Models;
using MCF_TestWeb.ViewModels;

namespace MCF_TestWeb.Services.Interface
{
    public interface IBPKBService
    {
        Task<ResponseDto?> GetAllBpkbAsync();
        Task<ResponseDto?> GetAllLocations();
        Task<ResponseDto?> CreateBpkbAsync(BpkbAddViewModel data);
        Task<ResponseDto?> UpdateBpkbAsync(BpkbAddViewModel data);
        Task<ResponseDto?> GetBpkbByID(string id);
        Task<ResponseDto?> DeleteBpkb(string id);

    }
}

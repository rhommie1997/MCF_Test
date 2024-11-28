using MCF_TestWeb.Models;
using MCF_TestWeb.ViewModels;

namespace MCF_TestWeb.Services.Interface
{
    public interface ILocationService
    {
        Task<ResponseDto?> GetAllLocAsync();
        Task<ResponseDto?> CreateLocAsync(LocationViewModel data);
        Task<ResponseDto?> UpdateLocAsync(LocationViewModel data);
        Task<ResponseDto?> GetLocByID(string id);
        Task<ResponseDto?> DeleteLoc(string id);
    }
}

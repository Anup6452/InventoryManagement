using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Services
{
    public interface IListServices
    {
        Task<List<SelectListItem>> ListGender();
        Task<List<SelectListItem>> ListRole();
        Task<List<SelectListItem>> ListCategory();
    }
}

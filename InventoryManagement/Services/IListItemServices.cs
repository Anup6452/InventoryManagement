using InventoryManagement.Entity;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Services
{
    public interface IListItemServices
    {
        Task<List<ListVM>> ListItem();
        void CreateItem(ListVM model);
        Task<ListVM> GetItemToEdit(string Id);
        void EditItem(string Id, ListVM model);
        ListItem DeleteItem(string Id);
        Task<List<SelectListItem>> ListCategory();
    }
}

using InventoryManagement.Entity;
using InventoryManagement.Models;

namespace InventoryManagement.Services
{
    public interface ICategoryServices
    {
        Task<List<CategoryVM>> ListCategory();
        void CreateCategory(CategoryVM model);

        Task<CategoryVM> GetCategoryToEdit(string Id);

        void EditCategory(string Id, CategoryVM model);

        ListItemCategory DeleteCategory(string Id);
    }
}

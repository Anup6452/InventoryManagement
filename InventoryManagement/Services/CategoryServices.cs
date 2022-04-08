using InventoryManagement.Data;
using InventoryManagement.Entity;
using InventoryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly InventoryDBContext _context;

        public CategoryServices(InventoryDBContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryVM>> ListCategory()
        {
            //var listCategory = _context.Category.Select(c => new CategoryVM
            //{
            //    ListItemCategoryId = c.ListItemCategoryId,
            //    ListItemCategoryName = c.ListItemCategoryName,
            //}).ToList();
            var listCategory = await (from cat in _context.Category
                               select new CategoryVM
                               {
                                   ListItemCategoryId = cat.ListItemCategoryId,
                                   ListItemCategoryName = cat.ListItemCategoryName,
                               }).ToListAsync();
            return listCategory;
        }

        public void CreateCategory(CategoryVM model)
        {
            var newCategory = new ListItemCategory
            {
                ListItemCategoryId = Guid.NewGuid().ToString(),
                ListItemCategoryName = model.ListItemCategoryName
            };
            _context.Add(newCategory);
            _context.SaveChanges();
        }

        public async Task<CategoryVM> GetCategoryToEdit(string Id)
        {
            //var getCategory = _context.Category.SingleOrDefault(x => x.ListItemCategoryId == Id);
            var getCategory = await (from cat in _context.Category where cat.ListItemCategoryId == Id select cat).SingleOrDefaultAsync();
            var result = new CategoryVM
            {
                ListItemCategoryId = getCategory.ListItemCategoryId,
                ListItemCategoryName = getCategory.ListItemCategoryName
            };
            return result;
        }

        public void EditCategory(string Id, CategoryVM model)
        {
            //var editCategory = _context.Category.SingleOrDefault(x => x.ListItemCategoryId == Id);
            var editCategory = (from cat in _context.Category where cat.ListItemCategoryId == Id select cat).SingleOrDefault();
            editCategory.ListItemCategoryName = model.ListItemCategoryName;
            _context.Update(editCategory);
            _context.SaveChanges();
        }

        public ListItemCategory DeleteCategory(string Id)
        {
            //var category = _context.Category.SingleOrDefault(x => x.ListItemCategoryId == Id);
            var category = (from cat in _context.Category where cat.ListItemCategoryId == Id select cat).SingleOrDefault();
            _context.Remove(category);
            _context.SaveChanges();
            return category;
        }
    }
}

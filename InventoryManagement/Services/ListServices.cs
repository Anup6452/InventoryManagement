using InventoryManagement.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class ListServices : IListServices
    {
        private readonly InventoryDBContext _context;

        public ListServices(InventoryDBContext context)
        {
            _context = context;
        }
        public async Task<List<SelectListItem>> ListGender()
        {
            //var categoryList = _context.Category.Select(x => new SelectListItem
            var genderList = await (from item in _context.ListItem
                              where item.ListItemCategory.ListItemCategoryName == "Gender"
                              select new SelectListItem
                              {
                                  Value = item.ListItemId,
                                  Text = item.ListItemName
                              }).ToListAsync();
            return genderList;
        }

        public async Task<List<SelectListItem>> ListRole()
        {
            //var categoryList = _context.Category.Select(x => new SelectListItem
            var roleList = await (from role in _context.Role
                            select new SelectListItem
                            {
                                Value = role.RoleId,
                                Text = role.RoleName
                            }).ToListAsync();
            return roleList;
        }

        public async Task<List<SelectListItem>> ListCategory()
        {
            //var categoryList = _context.Category.Select(x => new SelectListItem
            var categoryList = await (from category in _context.Category
                                      select new SelectListItem
                                      {
                                          Value = category.ListItemCategoryId,
                                          Text = category.ListItemCategoryName
                                      }).ToListAsync();
            return categoryList;
        }
    }
}

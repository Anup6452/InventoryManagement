using InventoryManagement.Data;
using InventoryManagement.Entity;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class ListItemServices : IListItemServices
    {
        private readonly InventoryDBContext _context;

        public ListItemServices(InventoryDBContext context)
        {
            _context = context;
        }

        public async Task<List<ListVM>> ListItem()
        {
            //var listItem = _context.ListItem.Select(c => new ListVM
            var listItem = await (from item in _context.ListItem join category in _context.Category 
                        on item.ListItemCategoryId equals category.ListItemCategoryId
                        select new ListVM
                           {
                               ListItemId = item.ListItemId,
                               ListItemName = item.ListItemName,
                               ListItemCategory = category,
                               ListItemCategoryId = item.ListItemCategoryId,
                           }).ToListAsync();
            return listItem;
        }

        public void CreateItem(ListVM model)
        {
            var newCList = new ListItem
            {
                ListItemId = Guid.NewGuid().ToString(),
                ListItemName = model.ListItemName,
                ListItemCategoryId = model.ListItemCategoryId,
            };
            _context.Add(newCList);
            _context.SaveChanges();
        }

        public async Task<ListVM> GetItemToEdit(string Id)
        {
            //var getItem = _context.ListItem.SingleOrDefault(x => x.ListItemId == Id);
            var getItem = await (from item in _context.ListItem where item.ListItemId == Id select item).SingleOrDefaultAsync();
            var result = new ListVM
            {
                ListItemId = getItem.ListItemId,
                ListItemName = getItem.ListItemName
            };
            return result;
        }

        public void EditItem(string Id, ListVM model)
        {
            //var editListItem = _context.ListItem.SingleOrDefault(x => x.ListItemId == Id);
            var editListItem = (from list in _context.ListItem where list.ListItemId == Id select list).SingleOrDefault();
            editListItem.ListItemName = model.ListItemName;
            _context.Update(editListItem);
            _context.SaveChanges();
        }

        public ListItem DeleteItem(string Id)
        {
            var deleteItem = (from list in _context.ListItem where list.ListItemId == Id select list).SingleOrDefault();
            _context.Remove(deleteItem);
            _context.SaveChanges();
            return deleteItem;
        }

        public async Task<List<SelectListItem>> ListCategory()
        {
            //var categoryList = _context.Category.Select(x => new SelectListItem
            var categoryList = await (from category in _context.Category select new SelectListItem
            {
                Value = category.ListItemCategoryId,
                Text = category.ListItemCategoryName
            }).ToListAsync();
            return categoryList;
        }

    }
}

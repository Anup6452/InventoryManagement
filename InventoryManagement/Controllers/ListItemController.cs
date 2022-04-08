using InventoryManagement.Entity;
using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class ListItemController : Controller
    {
        private readonly IListItemServices _listItemServices;

        public ListItemController(IListItemServices listItemServices)
        {
            _listItemServices = listItemServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> GetItemData()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                 List<ListVM> listItem = await _listItemServices.ListItem();
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    listItem = listItem.OrderBy(s => sortColumn + " " + sortColumnDirection).ToList();
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    listItem = listItem.Where(m => m.ListItemName.Contains(searchValue)).ToList();
                }
                recordsTotal = listItem.Count();
                var data = listItem.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> Create()
        {
            var categoryList = await _listItemServices.ListCategory();
            ViewBag.Category = categoryList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(ListVM model)
        {
            _listItemServices.CreateItem(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string Id)
        {
            var editCategory = await _listItemServices.GetItemToEdit(Id);
            var categoryList = await _listItemServices.ListCategory();
            ViewBag.Category = categoryList;
            return View(editCategory);
        }
        [HttpPost]
        public IActionResult Edit(string Id, ListVM model)
        {
            _listItemServices.EditItem(Id, model);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string Id)
        {
            ListItem itemToDelete = _listItemServices.DeleteItem(Id);
            if (itemToDelete != null)
            {
                return Json(new { success = true, message = "Deleted Successfully" });
            }
            return Json(new { success = false, message = "Something Went Wrong" });
        }
    }
}
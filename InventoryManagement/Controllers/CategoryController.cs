using InventoryManagement.Entity;
using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        public async Task<IActionResult> Index()
        {
            var category = await _categoryServices.ListCategory();
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> GetCategoryData()
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
                List<CategoryVM> listItem = await _categoryServices.ListCategory();
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    listItem = listItem.OrderBy(s => sortColumn + " " + sortColumnDirection).ToList();
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    listItem = listItem.Where(m => m.ListItemCategoryName.Contains(searchValue)).ToList();
                }
                recordsTotal = listItem.Count();
                var data = listItem.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryVM model)
        {
            _categoryServices.CreateCategory(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string Id)
        {
            var editCategory = await _categoryServices.GetCategoryToEdit(Id);
            return View(editCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string Id, CategoryVM model)
        {
            _categoryServices.EditCategory(Id, model);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string Id)
        {
            ListItemCategory categoryToDelete = _categoryServices.DeleteCategory(Id);
            if (categoryToDelete != null)
            {
                return Json(new { success = true, message = "Deleted Successfully" });
            }
            return Json(new { success = false, message = "Something Went Wrong" });
        }
    }
}

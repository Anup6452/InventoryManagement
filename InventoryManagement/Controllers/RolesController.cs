using InventoryManagement.Data;
using InventoryManagement.Entity;
using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RolesController : Controller
    {
        private readonly InventoryDBContext _context;
        private readonly IRolesServices _rolesServices;

        public RolesController(InventoryDBContext context, IRolesServices rolesServices)
        {
            _context = context;
            _rolesServices = rolesServices;
        }
        public IActionResult Index()
        {
            //var roles = _rolesServices.GetRoles();
            //return View(roles);
            return View();
        }

        [HttpPost]
        public IActionResult ListRole()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumn = Request.Form["order[0][column]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                var jsonData = _rolesServices.GetRolesForDataTable(draw, start, length, sortColumn, sortColumnDirection, searchValue, pageSize, skip);
                return Ok(jsonData.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoleVM model)
        {
            if (ModelState.IsValid)
            {
                _rolesServices.CreateRole(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public JsonResult IfRoleExist(string RoleName, string RoleId)
        {
            return Json(_rolesServices.IsUnique(RoleName, RoleId));
        }


        public async Task<IActionResult> Edit(string Id)
        {
            var role = await _rolesServices.GetRoleToEdit(Id);
            return View(role);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string Id, RoleVM model)
        {
            _rolesServices.EditRole(Id, model);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string Id)
        {
            Role roleToDelete = _rolesServices.DeleteRole(Id);
            if (roleToDelete != null)
            {
                return Json(new { success = true, message = "Deleted Successfully" });
            }
            return Json(new { success = false, message = "Something Went Wrong" });
        }
    }
}

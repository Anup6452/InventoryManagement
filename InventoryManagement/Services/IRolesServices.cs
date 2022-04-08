using InventoryManagement.Entity;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Services
{
    public interface IRolesServices
    {
        Task<List<RoleVM>> GetRoles();
        bool IsUnique(string RoleName, string RoleId);
        void CreateRole(RoleVM model);
        Task<RoleVM> GetRoleToEdit(string Id);
        void EditRole(string Id, RoleVM model);
        Role DeleteRole(string Id);
        JsonResult GetRolesForDataTable(string draw, string start, string length, string sortColumn, string sortColumnDirection, string searchValue, int pageSize, int skip);

    }
}

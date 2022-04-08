using InventoryManagement.Data;
using InventoryManagement.Entity;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class RoleServices : IRolesServices
    {
        private readonly InventoryDBContext _context;

        public RoleServices(InventoryDBContext context)
        {
            _context = context;
        }

        public async Task<List<RoleVM>> GetRoles()
        {
            //var roles = _context.Role.Select(x => new RoleVM
            //{
            //    RoleId = x.RoleId,
            //    RoleName = x.RoleName,
            //}).ToList();
            var roles = await (from role in _context.Role select new RoleVM
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
            }).ToListAsync();
            return roles;
        }

        public bool IsUnique(string RoleName, string RoleId)
        {
            if (RoleId == null) // its a new object
            {
                return !_context.Role.Any(x => x.RoleName.ToLower() == RoleName.ToLower());
            }
            else // its an existing object so exclude existing objects with the id
            {
                return !_context.Role.Any(x => x.RoleName.ToLower() == RoleName.ToLower() && x.RoleId != RoleId);
            }
        }

        public void CreateRole(RoleVM model)
        {
            var newRole = new Role
            {
                RoleId = Guid.NewGuid().ToString(),
                RoleName = model.RoleName
            };
            _context.Add(newRole);
            _context.SaveChanges();
        }

        public async Task<RoleVM> GetRoleToEdit(string Id)
        {
            //var getRole = _context.Role.SingleOrDefault(x => x.RoleId == Id);
            var getRole = await (from role in _context.Role where role.RoleId == Id select role).SingleOrDefaultAsync();
            var result = new RoleVM
            {
                RoleId = getRole.RoleId,
                RoleName = getRole.RoleName
            };
            return result;
        }

        public void EditRole(string Id, RoleVM model)
        {
            //var editRole = _context.Role.SingleOrDefault(x => x.RoleId == Id);
            var editRole = (from role in _context.Role where role.RoleId == Id select role).SingleOrDefault();
            editRole.RoleName = model.RoleName;
            _context.Update(editRole);
            _context.SaveChanges();
        }

        public Role DeleteRole(string Id)
        {
            //var role = _context.Role.SingleOrDefault(x => x.RoleId == Id);
            var deleteRole = (from role in _context.Role where role.RoleId == Id select role).SingleOrDefault();
            _context.Remove(deleteRole);
            _context.SaveChanges();
            return deleteRole;
        }

        public JsonResult GetRolesForDataTable(string draw, string start, string length, string sortColumn, string sortColumnDirection, string searchValue, int pageSize, int skip)
        {
            int recordsTotal = 0;
            List<Role> roleList = new List<Role>();
            roleList = (from role in _context.Role select role).Skip(skip).Take(pageSize).ToList();
            //roleList = _appDbContext.Roles.Skip(skip).Take(pageSize).ToList();
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                roleList = roleList.OrderByDescending(s => sortColumn + " " + sortColumnDirection).ToList();
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                roleList = roleList.Where(r => r.RoleId.ToString().Contains(searchValue.ToLower()) || r.RoleName.ToLower().Contains(searchValue.ToLower())).ToList();

            }
            recordsTotal = _context.Role.Count();
            var data = roleList;
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return new JsonResult(jsonData);
        }
    }
}

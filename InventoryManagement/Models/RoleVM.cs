using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Models
{
    public class RoleVM
    {
        public string? RoleId { get; set; }
        [Remote("IfRoleExist", "Roles", AdditionalFields = "RoleId", HttpMethod = "POST", ErrorMessage = "Role already exist")]
        public string RoleName { get; set; }
    }
}

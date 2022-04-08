using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class RoleVM
    {
        public string? RoleId { get; set; }
        [Required(ErrorMessage = "Please Enter Role Name")]
        [Remote("IfRoleExist", "Roles", AdditionalFields = "RoleId", HttpMethod = "POST", ErrorMessage = "Role already exist")]
        public string RoleName { get; set; }
    }
}

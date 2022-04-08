using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Entity
{
    [Index(nameof(RoleName), IsUnique = true)]
    public class Role
    {
        [Key]
        public string RoleId { get; set; }
        [Required(ErrorMessage = "Please Enter Role Name")]
        public string RoleName { get; set; }
    }
}

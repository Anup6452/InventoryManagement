using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Entity
{
    [Index(nameof(RoleName), IsUnique = true)]
    public class Role
    {
        [Key]
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}

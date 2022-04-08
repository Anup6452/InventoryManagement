using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Entity
{
    [Index(nameof(Email), IsUnique = true)]
    public class Employee
    {
        [Key]
        public string EmployeeId { get; set; }
        public string PersonId { get; set; }
        public Person Person { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
    }
}

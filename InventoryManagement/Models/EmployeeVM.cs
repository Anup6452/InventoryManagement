using InventoryManagement.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class EmployeeVM
    {
        public string EmployeeId { get; set; }
        public string PersonId { get; set; }
        public Person Person { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }

        //Person
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string GenderListItemId { get; set; }
        [ForeignKey("GenderListItemId")]
        public ListItem ListItem { get; set; }
    }
}

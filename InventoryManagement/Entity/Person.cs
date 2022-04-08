using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Entity
{
    [Index(nameof(ContactNo), IsUnique = true)]
    public class Person
    {
        [Key]
        public string PersonId { get; set; }
        [Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Contact number")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please Select the Gender")]
        public string GenderListItemId { get; set; }
        [ForeignKey("GenderListItemId")]
        public ListItem ListItem { get; set; }
    }
}

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

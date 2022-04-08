using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Entity
{
    [Index(nameof(ListItemId), IsUnique = true)]
    [Index(nameof(ListItemName), IsUnique = true)]
    public class ListItem
    {
        [Key]
        public string ListItemId { get; set; }
        [Required(ErrorMessage = "Please Select the Category.")]
        public string ListItemCategoryId { get; set; }
        public ListItemCategory ListItemCategory { get; set; }
        [Required(ErrorMessage = "Please Enter Item Name.")]
        public string ListItemName { get; set; }
    }
}

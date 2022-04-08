using InventoryManagement.Entity;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class ListVM
    {
        public string ListItemId { get; set; }
        [Required(ErrorMessage = "Please Select the Category")]
        public string ListItemCategoryId { get; set; }
        public ListItemCategory ListItemCategory { get; set; }
        [Required(ErrorMessage = "Please Enter Item Name")]
        public string ListItemName { get; set; }
        public string CategoryName { get; set; }
    }
}

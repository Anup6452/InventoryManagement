using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class CategoryVM
    {
        public string ListItemCategoryId { get; set; }
        [Required(ErrorMessage = "Please Enter Category Name")]
        public string ListItemCategoryName { get; set; }
    }
}

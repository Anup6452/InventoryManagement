using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Entity
{
    [Index(nameof(ListItemCategoryId), IsUnique = true)]
    [Index(nameof(ListItemCategoryName), IsUnique = true)]
    public class ListItemCategory
    {
        [Key]
        public string ListItemCategoryId { get; set; }
        public string ListItemCategoryName { get; set; }
        //public ICollection<ListItem> ListItems { get; set; }
    }
}

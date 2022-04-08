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

        public string ListItemCategoryId { get; set; }
        public ListItemCategory ListItemCategory { get; set; }
        public string ListItemName { get; set; }
    }
}

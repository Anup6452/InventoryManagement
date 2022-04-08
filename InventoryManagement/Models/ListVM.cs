using InventoryManagement.Entity;

namespace InventoryManagement.Models
{
    public class ListVM
    {
        public string ListItemId { get; set; }

        public string ListItemCategoryId { get; set; }
        public ListItemCategory ListItemCategory { get; set; }
        public string ListItemName { get; set; }
        public string CategoryName { get; set; }
    }
}

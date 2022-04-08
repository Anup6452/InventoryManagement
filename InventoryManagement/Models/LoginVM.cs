using InventoryManagement.Entity;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Invalid Email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

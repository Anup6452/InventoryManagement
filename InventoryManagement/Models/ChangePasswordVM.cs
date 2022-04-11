using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class ChangePasswordVM
    {
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [Compare("NewPassword", ErrorMessage = "The Password does not match.")]
        public string ConfirmPassword { get; set; }

    }
}

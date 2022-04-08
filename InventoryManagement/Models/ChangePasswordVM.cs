using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class ChangePasswordVM
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "The Password does not match.")]
        public string ConfirmPassword { get; set; }

    }
}

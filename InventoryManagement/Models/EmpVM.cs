using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class EmpVM
    {
        public string? PersonId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string ContactNo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string GenderListItemId { get; set; }


        //Employee
        public string? EmployeeId { get; set; }
        [Required]
        [Remote("checkEmail", "Employee", AdditionalFields = "EmployeeId", HttpMethod = "POST", ErrorMessage = "Email already exist")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "The Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string? RoleId { get; set; }
    }
}

using InventoryManagement.Entity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Models
{
    public class RegisterVM
    {
        //Person
        public string? PersonId { get; set; }
        [Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Contact Number")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Select Gender")]
        public string GenderListItemId { get; set; }


        //Employee
        public string? EmployeeId { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [Remote("checkEmail", "Account", AdditionalFields = "EmployeeId", HttpMethod = "POST", ErrorMessage = "Email already exist")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [Compare("Password", ErrorMessage = "The Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Select Role")]
        public string RoleId { get; set; }
    }
}

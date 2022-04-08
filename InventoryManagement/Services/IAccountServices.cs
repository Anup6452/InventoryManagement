using InventoryManagement.Entity;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Services
{
    public interface IAccountServices
    {
        Task<Employee> AuthenticateUser(string Email, string Password);
        Task<List<SelectListItem>> ListGender();
        void Register(RegisterVM model);
        Task<List<SelectListItem>> ListRole();
        bool IfEmailExists(string email);
        Employee GetUserToChangePassword(string Id);
        Employee ChangePassword(Employee employee);
    }
}

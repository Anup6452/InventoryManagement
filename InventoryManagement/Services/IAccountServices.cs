using InventoryManagement.Entity;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Services
{
    public interface IAccountServices
    {
        Task<Employee> AuthenticateUser(string Email, string Password);
        void Register(RegisterVM model);
        bool IfEmailExists(string email);
        Employee GetUserToChangePassword(string Id);
        Employee ChangePassword(Employee employee);
    }
}

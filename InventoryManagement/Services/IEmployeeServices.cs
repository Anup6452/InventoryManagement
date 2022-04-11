using InventoryManagement.Entity;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryManagement.Services
{
    public interface IEmployeeServices
    {
        List<EmployeeVM> GetAllEmployee();
        void CreateEmployee(EmpVM model);
        bool IfEmailExists(string email);
        EmpVM GetEmployeeToEdit(string Id);
        void EditEmployee(string Id, EmpVM model);
        Employee DeleteEmployee(string Id);
    }
}

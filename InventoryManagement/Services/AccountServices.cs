using InventoryManagement.Data;
using InventoryManagement.Entity;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InventoryManagement.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly InventoryDBContext _context;

        public AccountServices(InventoryDBContext context)
        {
            _context = context;
        }


        public async Task<Employee> AuthenticateUser(string Email, string Password)
        {
            //Employee employee = _context.Employee.Where(x => x.Email == Email && x.Password == Password).Include(x => x.Person).Include(x => x.Role).FirstOrDefault();
            var employee = await (from emp in _context.Employee
                            where emp.Email == Email && emp.Password == Hash.GetHash(Password)
                            join per in _context.Person on emp.PersonId equals per.PersonId
                            join r in _context.Role on emp.RoleId equals r.RoleId
                            select new Employee
                            {
                                EmployeeId = emp.EmployeeId,
                                Email = emp.Email,
                                Role = r,
                                Person = per,
                            } ).FirstOrDefaultAsync();
            return employee;
        }

        public async Task<List<SelectListItem>> ListGender()
        {
            var gender = await _context.ListItem.Where(x => x.ListItemCategoryId == "Cat-01").Select(x => new SelectListItem
            {
                Value = x.ListItemId,
                Text = x.ListItemName
            }).ToListAsync();
            return gender;
        }
        
        public async Task<List<SelectListItem>> ListRole()
        {
            var role = await _context.Role.Select(x => new SelectListItem
            {
                Value = x.RoleId,
                Text = x.RoleName
            }).ToListAsync();
            return role;
        }


        public void Register(RegisterVM model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var roleId = _context.Role.Where(x => x.RoleName == "User").Select(x => x.RoleId).SingleOrDefault();
                    Person person = new Person()
                    {
                        PersonId = Guid.NewGuid().ToString(),
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName == null ? "" : model.MiddleName,
                        LastName = model.LastName,
                        ContactNo = model.ContactNo,
                        Address = model.Address,
                        GenderListItemId = model.GenderListItemId,
                    };
                    Employee employee = new Employee()
                    {
                        EmployeeId = Guid.NewGuid().ToString(),
                        PersonId = person.PersonId,
                        Email = model.Email,
                        Password = Hash.GetHash(model.Password),
                        RoleId = roleId,
                    };
                    _context.Person.Add(person);
                    _context.Employee.Add(employee);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();

                }
            }
        }
        public bool IfEmailExists(string email)
        {
            var result = !_context.Employee.Any(x => x.Email == email);
            return result;
        }


        public Employee ChangePassword(Employee employee)
        {
            Employee updatePassword = _context.Employee.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
            if (updatePassword != null)
            {
                updatePassword.Password = employee.Password;
                _context.Employee.Update(updatePassword);
                _context.SaveChanges();
            }
            return updatePassword;
        }

        public Employee GetUserToChangePassword(string Id)
        {
            
            Employee employeeToDelete = (from employee in _context.Employee
                                         where employee.EmployeeId == Id
                                         select employee).FirstOrDefault();
            return employeeToDelete;
        }
    }
}

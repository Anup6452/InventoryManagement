using InventoryManagement.Data;
using InventoryManagement.Entity;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly InventoryDBContext _context;

        public EmployeeServices(InventoryDBContext context)
        {
            _context = context;
        }

        public List<EmployeeVM> GetAllEmployee()
        {
            var employeeList = (from employee in _context.Employee
                            join person in _context.Person on employee.PersonId equals person.PersonId 
                            join role in _context.Role on employee.RoleId equals role.RoleId
                            join item in _context.ListItem on person.GenderListItemId equals item.ListItemId
                            select new EmployeeVM
                            {
                                FullName = string.Concat(person.FirstName, " ", person.MiddleName, " ",person.LastName),
                                Person = person,
                                EmployeeId = employee.EmployeeId,
                                Email = employee.Email,
                                Password = employee.Password,
                                Role = role,
                                ListItem = item,
                            }).ToList();
            return employeeList;
        }

        public void CreateEmployee(EmpVM model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
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
                        RoleId = model.RoleId,
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

        public EmpVM GetEmployeeToEdit(string Id)
        {
            //var getItem = _context.ListItem.SingleOrDefault(x => x.ListItemId == Id);
            var getEmployee = (from employee in _context.Employee where employee.EmployeeId == Id 
                                     join person in _context.Person on employee.PersonId equals person.PersonId
                                     select new EmpVM
                                     {
                                         EmployeeId = employee.EmployeeId,
                                         FirstName = person.FirstName,
                                         MiddleName = person.MiddleName,
                                         LastName = person.LastName,
                                         ContactNo = person.ContactNo,
                                         Address = person.Address,
                                         GenderListItemId = person.GenderListItemId,
                                         Email = employee.Email,
                                         RoleId = employee.RoleId,
                                     }).SingleOrDefault();
            return getEmployee;
        }

        public void EditEmployee(string Id, EmpVM model)
        {
            //Employee editEmployee = GetEmployeeToEdit(Id);
            //editEmployee.Person.FirstName = model.FirstName;
            //editEmployee.Person.MiddleName = model.MiddleName;
            //editEmployee.Person.LastName = model.LastName;
            //editEmployee.Email = model.Email;
            //editEmployee.Person.ContactNo = model.ContactNo;
            //editEmployee.Person.Address = model.Address;
            //editEmployee.Person.GenderListItemId = model.GenderListItemId;
            //editEmployee.RoleId = model.RoleId;
            //var editEmployee = GetEmployeeToEdit(Id);
            //Employee employee = Employee{ 
            //    editEmployee.Email = model.Email,
            //}
            //_context.Employee.Update(editEmployee);
            var editEmployee = _context.Employee.Include(x => x.Person).Include(x => x.Role).SingleOrDefault(x => x.EmployeeId == Id);
            if (editEmployee != null)
            {
                editEmployee.Person.FirstName = model.FirstName;
                editEmployee.Person.MiddleName = model.MiddleName == null ? "" : model.MiddleName;
                editEmployee.Person.LastName = model.LastName;
                editEmployee.Person.ContactNo = model.ContactNo;
                editEmployee.Person.Address = model.Address;
                editEmployee.Person.GenderListItemId = model.GenderListItemId;
                editEmployee.Email = model.Email;
                editEmployee.RoleId = model.RoleId;
            }
            _context.Employee.Update(editEmployee);
            _context.SaveChanges();
        }

        public Employee DeleteEmployee(string Id)
        {
            //var category = _context.Category.SingleOrDefault(x => x.ListItemCategoryId == Id);
            var employee = (from emp in _context.Employee where emp.EmployeeId == Id select emp).SingleOrDefault();
            _context.Remove(employee);
            _context.SaveChanges();
            return employee;
        }
    }
}

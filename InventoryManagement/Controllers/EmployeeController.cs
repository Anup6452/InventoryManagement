﻿using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetEmployeeData()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                List<EmployeeVM> listItem = _employeeServices.GetAllEmployee();
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    listItem = listItem.OrderBy(s => sortColumn + " " + sortColumnDirection).ToList();
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    listItem = listItem.Where(m => m.FullName.Contains(searchValue) || m.Email.Contains(searchValue)).ToList();
                }
                recordsTotal = listItem.Count();
                var data = listItem.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult CreateEmployee()
        {
            var genderList = _employeeServices.ListGender();
            var roleList = _employeeServices.ListRole();
            ViewBag.Gender = genderList;
            ViewBag.Role = roleList;
            return View();
        }
        [HttpPost]
        public IActionResult CreateEmployee(EmpVM model)
        {
            if (ModelState.IsValid)
            {
                _employeeServices.CreateEmployee(model);
                return RedirectToAction("Index");
            }
            var genderList = _employeeServices.ListGender();
            var roleList = _employeeServices.ListRole();
            ViewBag.Gender = genderList;
            ViewBag.Role = roleList;
            return View();
        }

        [HttpPost]
        public JsonResult checkEmail(string EmployeeId, string Email)
        {
            if (EmployeeId == null)
            {
                return Json(_employeeServices.IfEmailExists(Email));
            }
            else
            {
                var emp = _employeeServices.GetEmployeeToEdit(EmployeeId);
                if (emp.Email == Email)
                {
                    return Json(true);
                }
                return Json(_employeeServices.IfEmailExists(Email));
            }
            
        }

        public IActionResult EditEmployee(string Id)
        {
            
            var employee = _employeeServices.GetEmployeeToEdit(Id);
            var genderList = _employeeServices.ListGender();
            var roleList = _employeeServices.ListRole();
            ViewBag.Gender = genderList;
            ViewBag.Role = roleList;
            return View(employee);
        }
        [HttpPost]
        public IActionResult EditEmployee(string Id, EmpVM model)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");
            if (ModelState.IsValid)
            {
                _employeeServices.EditEmployee(Id, model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("EditEmployee");
        }
    }
}
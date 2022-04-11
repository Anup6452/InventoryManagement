using InventoryManagement.Data;
using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventoryManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly InventoryDBContext _context;
        private readonly IAccountServices _accountServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly SignInManager<IdentityUser> _signInManager;
        //UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager
        //_userManager = userManager;
        //_signInManager = signInManager;

        public AccountController(InventoryDBContext context, IAccountServices accountServices, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _accountServices = accountServices;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult login()
        {
            return User.Identity is not null && User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model, string? ReturnUrl)
        {

            if (ModelState.IsValid)
            {
                var user = await _accountServices.AuthenticateUser(model.Email, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    ViewBag.Error = "Invalid login attempt.";
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim("UserId", user.EmployeeId),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("FullName", string.Concat(user.Person.FirstName, " ", user.Person.MiddleName, " ", user.Person.LastName)),
                    new Claim(ClaimTypes.Role, user.Role.RoleName),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {

                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }

            // Something failed. Redisplay the form.
            return View();
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> Register()
        {
            var genderList = await _accountServices.ListGender();
            var roleList = await _accountServices.ListRole();
            ViewBag.Gender = genderList;
            ViewBag.Role = roleList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                _accountServices.Register(model);
                return RedirectToAction("Login");
            }
            var genderList = await _accountServices.ListGender();
            var roleList = await _accountServices.ListRole();
            ViewBag.Gender = genderList;
            ViewBag.Role = roleList;
            return View(model);
        }

        [HttpPost]
        public JsonResult checkEmail(string Email)
        {
            return Json(_accountServices.IfEmailExists(Email));
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
        
        [HttpPost][Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

                var employee = _accountServices.GetUserToChangePassword(userId);
                if (employee.Password != Hash.GetHash(model.Password))
                {
                    ViewBag.Error = "Current Password does not match.";
                    return View(model);
                }
                employee.Password = Hash.GetHash(model.ConfirmPassword);
                var result = _accountServices.ChangePassword(employee);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                TempData["Message"] = "Password Successfully Changed";
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }
    }
}

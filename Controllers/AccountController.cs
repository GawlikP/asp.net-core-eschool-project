using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using lista_7.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace lista_7.Controllers
{
    public class AccountController : Controller
    {

        protected readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
          
        }
        [Authorize(Roles="Admin")]
        public IActionResult Accounts()
        {
            var temp = _db.Users.ToList();

            foreach (var st in temp)
            {
                ViewData[st.Id.ToString()] = _db.Roles.Find(st.RoleID).RoleName;
            }
            return View(temp);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            String str = "";
            var Roles = await _db.Roles.ToListAsync();
            foreach (var role in Roles)
            {
                str += role.RoleName.ToString() + ",";
            }

            ViewData["Roles"] = str;
            

            User usr = await _db.Users.FindAsync(id);
            if(usr == null)
            {
                return NotFound();
            }
            var rol = await _db.Roles.FindAsync(usr.RoleID);
            ViewData["ActualRole"] = rol.RoleName.ToString();
            return View(usr);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(User user, string roleName)
        {
            if (ModelState.IsValid)
            {
                var tuser = await _db.Users.FindAsync(user.Id);
                var role = _db.Roles.FirstOrDefault(r => r.RoleName == roleName);
                tuser.Name = user.Name;
                tuser.LastName = user.LastName;
                tuser.Password = user.Password;
                tuser.Role = role;
                tuser.RoleID = role.Id;
                tuser.Subjects = user.Subjects;
                tuser.ActivationCode = user.ActivationCode;
                tuser.Email = user.Email;
                tuser.GivenGrades = user.GivenGrades;

                await _db.SaveChangesAsync();
                return RedirectToAction("Accounts");
            }
            else
            {
                return RedirectToAction("Edit", new { id = user.Id });
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            String str = "";
            var Roles = _db.Roles.ToList();
            foreach(var role in Roles)
            {
                str += role.RoleName.ToString() + ",";
            }

            ViewData["Roles"] = str;

            return View(new User());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(User user, string roleName)
        {
            if (ModelState.IsValid)
            {
                var r = _db.Users.FirstOrDefault(u => u.Username == user.Username);
                if (r == null)
                {
                    var role = _db.Roles.FirstOrDefault(r => r.RoleName == roleName);
                    user.Role = role;
                    user.RoleID = role.Id;
                    await _db.Users.AddAsync(user);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Accounts", "Account");
                }
                else
                {
                    ModelState.AddModelError("CustomError", "Username");
                    ViewData["Error"] = "Already exist user with that username";
                    return RedirectToAction("Accounts");
                }
            }
            else
            {
                ModelState.AddModelError("CustomError", "Username Exist");
                return RedirectToAction("Accounts");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            _db.Remove(user);
            await _db.SaveChangesAsync();
            return RedirectToAction("Accounts");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddRole()
        {
            var role = new Role();

            return View(role);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddRole(Role role)
        {
            if (ModelState.IsValid)
            {
                var r = _db.Roles.FirstOrDefault(r => r.RoleName == role.RoleName);
                if (r == null)
                {
                    await _db.Roles.AddAsync(role);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("CustomError", "Role name exist");
                    return RedirectToAction("AddRole");
                }
            }
            else
            {
                ModelState.AddModelError("CustomError", "Role name exist");
                return RedirectToAction("AddRole");
            }
        }

        public IActionResult Register()
        {
            var user = new User();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {

            if (ModelState.IsValid)
            {
                var r = _db.Users.FirstOrDefault(u => u.Username == user.Username);
                if (r == null)
                {
                    var role  = _db.Roles.FirstOrDefault(r=>r.RoleName == "User");
                    user.Role = role;
                    user.RoleID = role.Id;
                    await _db.Users.AddAsync(user);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("CustomError", "Username");
                    return RedirectToAction("Register");
                }
            }
            else
            {
                ModelState.AddModelError("CustomError", "Username Exist");
                return RedirectToAction("Register");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new User());
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            
            var sr = await _db.Users.FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);
            if(sr == null)
            {
                ModelState.AddModelError("", "User not found");
                return RedirectToAction("Login");
            }
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, sr.Username));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, sr.Name));
            identity.AddClaim(new Claim(ClaimTypes.Surname, sr.LastName));
            var role = await _db.Roles.FindAsync(sr.RoleID);
            identity.AddClaim(new Claim(ClaimTypes.Role, role.RoleName));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            HttpContext.Session.SetString("Name", sr.Username);

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Name");
            return RedirectToAction("Login");
        }
      
        
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult NotEnoughPriorites()
        {
            return View();
        }
    }
}
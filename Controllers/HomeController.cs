using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using lista_7.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace lista_7.Controllers
{
    [Authorize(Roles = "User,Teacher, Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _db;
        

        public HomeController(ApplicationDbContext db,ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
         
           

        }
        [Authorize(Roles = "User,Teacher, Admin")]
        public IActionResult Index()
        {
           

            return View();
        }
        [Authorize(Roles = "Teacher, Admin")]
        public async Task<IActionResult> Students()
        {
            var name = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            var role = await _db.Roles.FirstOrDefaultAsync(r => r.RoleName == "User");

            var students =  _db.Users.Where(b => b.RoleID == role.Id);
            IEnumerable < User >  st = students;

            return View(st);
        }

        [Authorize(Roles = "User, Teacher, Admin")]
        public async Task<IActionResult> Grades()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

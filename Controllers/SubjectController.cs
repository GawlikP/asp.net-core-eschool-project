using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lista_7.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lista_7.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SubjectController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var subjects = _db.Subjects.ToList();

            foreach(var sb in subjects)
            {
                var teacher = await _db.Users.FindAsync(sb.TeacherId);
                ViewData[sb.Id.ToString()] = teacher.Username.ToString();

            }

            return View(subjects);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var usrs = await _db.Users.ToListAsync();
            String str = "";
            String str2 = "";
            foreach (var user in usrs)
            {
                Role rl = await _db.Roles.FindAsync(user.RoleID);
                if (rl.RoleName == "Teacher")
                {
                    str += user.Username+",";
                    str2 += user.Id+",";
                }
            }
            ViewData["Teachers"] = str;
            ViewData["Teachers_ids"] = str2;
            
            var subject = await _db.Subjects.FindAsync(id);
            ViewData["ActualId"] = subject.TeacherId;
            return View(subject);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Subject sb, int userId)
        {
            if (ModelState.IsValid)
            {
                var subject = await _db.Subjects.FindAsync(sb.Id);
                if (subject != null)
                {
                    var teacher = await _db.Users.FindAsync(userId);
                    subject.Teacher = teacher;
                    subject.TeacherId = teacher.Id;
                    subject.Name = sb.Name;
                    
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("CustomError", "User does not exit");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("CustomError", "Not Valid");
                return RedirectToAction("Index");
            }
        }
       
        public async Task<IActionResult> Create()
        {
            var usrs = await _db.Users.ToListAsync();
            String str = "";
            String str2 = "";
            foreach(var user in usrs)
            {
                Role rl = await _db.Roles.FindAsync(user.RoleID);
                if (rl.RoleName == "Teacher"){
                    str += user.Username+",";
                    str2 += user.Id+",";
                }
            }
            ViewData["Teachers"] = str;
            ViewData["Teachers_ids"] = str2;
            return View(new Subject());
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Subject sb, int userId)
        {
            if (ModelState.IsValid)
            {
                var teacher = await _db.Users.FindAsync(userId);
                sb.Teacher = teacher;
                sb.TeacherId = teacher.Id;
                await _db.Subjects.AddAsync(sb);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("CustomError", "NotValid");
                return RedirectToAction("Create");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            _db.Remove(subject);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lista_7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lista_7.Controllers
{
    public class GradesScaleController : Controller
    {
        ApplicationDbContext _db;

        public GradesScaleController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var gradesScales = await _db.GradeScales.ToListAsync();

            return View(gradesScales);
        }
        public IActionResult Create()
        {
            return View(new GradeScale());
        }
        [HttpPost]
        public async Task<IActionResult> Create(GradeScale gs, int allowHalf)
        {
            if (ModelState.IsValid)
            {
                gs.AllowHalfGrades = allowHalf;
                await _db.GradeScales.AddAsync(gs);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            var gs = await _db.GradeScales.FindAsync(id);
            ViewData["allowHalf"] = gs.AllowHalfGrades.ToString();

            return View(gs);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(GradeScale gs, int allowHalf)
        {
            if (ModelState.IsValid)
            {
                GradeScale actgs = await _db.GradeScales.FindAsync(gs.Id);
                actgs.AllowHalfGrades = allowHalf;
                actgs.DownBorder = gs.DownBorder;
                actgs.UpBorder = gs.UpBorder;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            else
            {
                return RedirectToAction("Edit");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var gs = await _db.GradeScales.FindAsync(id);
            if (gs == null)
            {
                return NotFound();
            }
            
            _db.Remove(gs);
            await _db.SaveChangesAsync();
           
            return RedirectToAction("Index");
        }
    }
}
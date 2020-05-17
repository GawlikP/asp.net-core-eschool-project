using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lista_7.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lista_7.Controllers
{
    public class GradesController : Controller
    {
        ApplicationDbContext _db;
        public GradesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var grades = await _db.Grades.ToListAsync();
            foreach (var grade in grades)
            {
                User student = await _db.Users.FindAsync(grade.StudentId);
                ViewData["Student" + grade.Id] = student.Name + " " + student.LastName;
                User teacher = await _db.Users.FindAsync(grade.TecherId);
                ViewData["Teacher" + grade.Id] = teacher.Name + " " + teacher.LastName;
                Subject subject = await _db.Subjects.FindAsync(grade.SubjectId);
                ViewData["Subject" + grade.Id] = subject.Name;
                GradeScale gradeScale = await _db.GradeScales.FindAsync(grade.gradeScaleId);
                ViewData["Scale" + grade.Id] = gradeScale.DownBorder + "-" + grade.gradeScale.UpBorder;
            }

            return View(grades);
        }
        public IActionResult Create()
        {
            IEnumerable<User> Students = _db.Users.Where(u => u.RoleID == 1);
            IEnumerable<User> Teachers = _db.Users.Where(u => u.RoleID == 3);
            IEnumerable<Subject> Subjects = _db.Subjects.ToList();
            IEnumerable<GradeScale> Scales = _db.GradeScales.ToList();
            String students_ids = ""; String students_labels = ""; String teachers_ids = ""; String teachers_labels = "";
            String subjects_ids = ""; String subjects_labels = ""; String scales_ids = ""; String scales_labels = "";
            foreach (var stud in Students)
            {
                students_ids += stud.Id + ",";
                students_labels += stud.Name + " " + stud.LastName + ",";
            }
            foreach (var teach in Teachers)
            {
                teachers_ids += teach.Id + ",";
                teachers_labels += teach.Name + " " + teach.LastName + ",";
            }
            foreach (var sub in Subjects)
            {
                subjects_ids += sub.Id + ",";
                subjects_labels += sub.Name + ",";
            }
            foreach (var scal in Scales)
            {
                scales_ids += scal.Id + ",";
                scales_labels += scal.DownBorder + "-" + scal.UpBorder + ",";
            }
            ViewData["StudentIds"] = students_ids;
            ViewData["StudentLabels"] = students_labels;
            ViewData["TeacherIds"] = teachers_ids;
            ViewData["TeacherLabels"] = teachers_labels;
            ViewData["SubjectIds"] = subjects_ids;
            ViewData["SubjectLabels"] = subjects_labels;
            ViewData["ScaleIds"] = scales_ids;
            ViewData["ScaleLabels"] = scales_labels;
            ViewData["Error"] = HttpContext.Session.GetString("Error");
            HttpContext.Session.Remove("Error");

            return View(new Grade());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Grade grade, int studentId, int teacherId, int subjectId, int scaleId)
        {
            if (ModelState.IsValid)
            {
                var scale = await _db.GradeScales.FindAsync(scaleId);
                if (grade.Number <= scale.DownBorder && grade.Number >= scale.UpBorder)
                {
                    grade.StudentId = studentId;
                    grade.Student = await _db.Users.FindAsync(studentId);
                    grade.TecherId = teacherId;
                    grade.Teacher = await _db.Users.FindAsync(teacherId);
                    grade.SubjectId = subjectId;
                    grade.Subject = await _db.Subjects.FindAsync(subjectId);
                    grade.gradeScaleId = scaleId;
                    grade.gradeScale = scale;
                    await _db.Grades.AddAsync(grade);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Grade didnt mach the scale");
                    return RedirectToAction("Create");
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Doesnt match Validation");
                return RedirectToAction("Create");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var gr = await _db.Grades.FindAsync(id);
            if (gr != null)
            {
                IEnumerable<User> Students = _db.Users.Where(u => u.RoleID == 1);
                IEnumerable<User> Teachers = _db.Users.Where(u => u.RoleID == 3);
                IEnumerable<Subject> Subjects = await _db.Subjects.ToListAsync();
                IEnumerable<GradeScale> Scales = await _db.GradeScales.ToListAsync();
                String students_ids = ""; String students_labels = ""; String teachers_ids = ""; String teachers_labels = "";
                String subjects_ids = ""; String subjects_labels = ""; String scales_ids = ""; String scales_labels = "";
                foreach (var stud in Students)
                {
                    students_ids += stud.Id + ",";
                    students_labels += stud.Name + " " + stud.LastName + ",";
                }
                foreach (var teach in Teachers)
                {
                    teachers_ids += teach.Id + ",";
                    teachers_labels += teach.Name + " " + teach.LastName + ",";
                }
                foreach (var sub in Subjects)
                {
                    subjects_ids += sub.Id + ",";
                    subjects_labels += sub.Name + ",";
                }
                foreach (var scal in Scales)
                {
                    scales_ids += scal.Id + ",";
                    scales_labels += scal.DownBorder + "-" + scal.UpBorder + ",";
                }
                ViewData["StudentIds"] = students_ids;
                ViewData["StudentLabels"] = students_labels;
                ViewData["SStudentId"] = gr.StudentId;
                ViewData["TeacherIds"] = teachers_ids;
                ViewData["TeacherLabels"] = teachers_labels;
                ViewData["TTeacherId"] = gr.TecherId;
                ViewData["SubjectIds"] = subjects_ids;
                ViewData["SubjectLabels"] = subjects_labels;
                ViewData["SSubjectId"] = gr.SubjectId;
                ViewData["ScaleIds"] = scales_ids;
                ViewData["ScaleLabels"] = scales_labels;
                ViewData["SScaleId"] = gr.gradeScaleId;
                ViewData["Error"] = HttpContext.Session.GetString("Error");
                HttpContext.Session.Remove("Error");

                return View(gr);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Grade grade, int studentId, int teacherId, int subjectId, int scaleId)
        {
            if (ModelState.IsValid)
            {
                var scale = await _db.GradeScales.FindAsync(scaleId);
                Grade actGrade = await _db.Grades.FindAsync(grade.Id);
                if (grade.Number <= scale.DownBorder && grade.Number >= scale.UpBorder)
                {
                    actGrade.Number = grade.Number;
                    actGrade.Classification = grade.Classification;
                    actGrade.Weight = grade.Weight;
                    actGrade.StudentId = studentId;
                    actGrade.Student = await _db.Users.FindAsync(studentId);
                    actGrade.TecherId = teacherId;
                    actGrade.Teacher = await _db.Users.FindAsync(teacherId);
                    actGrade.SubjectId = subjectId;
                    actGrade.Subject = await _db.Subjects.FindAsync(subjectId);
                    actGrade.gradeScaleId = scaleId;
                    actGrade.gradeScale = scale;

                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    HttpContext.Session.SetString("Error", "Grade didnt mach the scale");
                    return RedirectToAction("Edit");
                }
            }
            else
            {
                HttpContext.Session.SetString("Error", "Doesnt match Validation");
                return RedirectToAction("Edit");
            }
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _db.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            _db.Remove(grade);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
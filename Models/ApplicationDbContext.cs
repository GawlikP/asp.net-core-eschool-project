using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


namespace lista_7.Models
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne<Role>(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleID);
            modelBuilder.Entity<Grade>().HasOne<User>(g => g.Student).WithMany(u => u.Grades).HasForeignKey(g => g.StudentId);
            modelBuilder.Entity<Grade>().HasOne<User>(g => g.Teacher).WithMany(u => u.GivenGrades).HasForeignKey(g => g.TecherId);
            modelBuilder.Entity<Grade>().HasOne<Subject>(g => g.Subject).WithMany(s => s.Grades).HasForeignKey(g => g.SubjectId);
            modelBuilder.Entity<Subject>().HasOne<User>(s => s.Teacher).WithMany(u => u.Subjects).HasForeignKey(s => s.TeacherId);
            modelBuilder.Entity<Grade>().HasOne<GradeScale>(g => g.gradeScale).WithMany(gs => gs.Grades).HasForeignKey(g => g.gradeScaleId);
           
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<GradeScale> GradeScales { get; set; }

    }
}

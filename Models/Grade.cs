using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lista_7.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Number { get; set; }
        [Required]
        public string Classification { get; set; }
        
        public User Student { get; set; }
        [Required]
        public int StudentId { get; set; }
        
        public User Teacher { get; set; }
        [Required]
        public int TecherId { get; set; }
        
        public Subject Subject { get; set; }
        [Required]
        public int SubjectId { get; set; }
       
        public GradeScale gradeScale {get; set;}
        [Required]
        public int gradeScaleId { get; set; }
        public double Weight { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lista_7.Models
{
    public class GradeScale
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UpBorder { get; set; }
        [Required]
        public int DownBorder { get; set; }
        [Required]
        public int AllowHalfGrades { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
}

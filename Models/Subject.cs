using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lista_7.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public User Teacher { get; set; }
        public int TeacherId { get; set; }
        public ICollection<Grade> Grades { get; set; }

    }
}

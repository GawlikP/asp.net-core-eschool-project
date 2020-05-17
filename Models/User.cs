using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lista_7.Models
{
    public class User
    {   
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        public Guid ActivationCode { get; set; }
        public Role Role { get; set; }
        public int RoleID { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Grade> Grades { get; set; }
        public ICollection<Grade> GivenGrades { get; set; }
    }
}

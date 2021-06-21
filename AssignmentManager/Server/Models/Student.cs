using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Server.Models
{
    public class Student
    {
        [Key] public int IsuId { get; set; }
        [Required] public string Name { get; set; }
        public string MiddleName { get; set; }
        [Required] public string Lastname { get; set; }
        [Required] public string Email { get; set; }
        public string Phone { get; set; }

        public int GroupId { get; set; }
        [Required] public Group Group { get; set; }
        public virtual IList<Solution> Solutions { get; set; }
    }
}
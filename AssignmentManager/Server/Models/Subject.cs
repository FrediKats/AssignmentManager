using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Server.Models
{
    public class Subject
    {
        /*[Key] public int SubjectId { get; set; }
        [Required] public string Name { get; set; }
        
        public virtual IList<Instructor> Instructors { get; set; }
        public virtual IList<Assignment> Assignments { get; set; }
        public virtual IList<Speciality> Specialities { get; set; }*/
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        //public IList<int> InstructorIds { get; set; } = new List<int>();
    }
}
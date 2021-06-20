using System.Collections.Generic;

namespace AssignmentManager.Server.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public IList<Instructor> Instructors { get; set; } = new List<Instructor>();
    }
}
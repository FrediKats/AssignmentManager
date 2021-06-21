using System.Collections.Generic;

namespace AssignmentManager.Server.Resources
{
    public class SubjectResource
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public IList<InstructorResource> Instructors { get; set; }
    }
}
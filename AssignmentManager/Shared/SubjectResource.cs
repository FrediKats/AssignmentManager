using System.Collections.Generic;

namespace AssignmentManager.Shared
{
    public class SubjectResource
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public virtual IList<InstructorResourceBriefly> Instructors { get; set; }
        public virtual IList<AssignmentResourceBriefly> Assignments { get; set; }
        public virtual IList<SpecialityResourceBriefly> Specialities { get; set; }
    }
}
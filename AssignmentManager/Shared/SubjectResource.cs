using System.Collections.Generic;

namespace AssignmentManager.Shared
{
    public class SubjectResource
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public IList<int> InstructorIds { get; set; }
    }
}
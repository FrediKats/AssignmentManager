using System.Collections.Generic;

namespace AssignmentManager.Shared
{
    public class StudentResource
    {
        public int IsuId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public GroupResourceBriefly Group { get; set; }
        public IList<SolutionResourceBriefly> Solutions { get; set; }
        public IList<SubjectResourceBriefly> Subjects { get; set; }
        public IList<AssignmentResourceBriefly> Assignments { get; set; }
        
    }
}
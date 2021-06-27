using System.Collections.Generic;

namespace AssignmentManager.Shared
{
    public class StudentResource
    {
        public StudentResource()
        {
            Solutions = new List<SolutionResourceBriefly>();
            Subjects = new List<SubjectResourceBriefly>();
            Assignments = new List<AssignmentResourceBriefly>();
        }
        public StudentResource(StudentResource st)
        {
            IsuId = st.IsuId;
            Name = st.Name;
            LastName = st.LastName;
            Email = st.Email;
            MiddleName = st.Email;
            Phone = st.Phone;
            Group = st.Group;
            Solutions = st.Solutions;
            Subjects = st.Subjects;
            Assignments = st.Assignments;
        }
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
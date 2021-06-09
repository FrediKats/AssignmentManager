using System.Collections.Generic;

namespace AssignmentManager.Server.Models
{
    public class Speciality
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public EStudyType StudyType { get; set; }

        public IList<Group> Groups { get; set; }
    }
}
using System.Collections.Generic;

namespace AssignmentManager.Server.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int SpecialityId { get; set; }
        public Speciality Speciality { get; set; }

        public IList<Student> Students { get; set; }
    }
}
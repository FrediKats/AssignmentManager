using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssignmentManager.Server.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SpecialityId { get; set; }
        public Speciality Speciality { get; set; }
        public virtual IList<Student> Students { get; set; }
    }
}
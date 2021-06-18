using System.Collections.Generic;
using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Resources
{
    public class GroupResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<StudentResourceBriefly> Students { get; set; }
        public SpecialityResourceBriefly Speciality { get; set; }
    }
}
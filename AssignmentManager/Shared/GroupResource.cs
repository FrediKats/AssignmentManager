using System.Collections.Generic;

namespace AssignmentManager.Shared
{
    public class GroupResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<StudentResourceBriefly> Students { get; set; }
        public SpecialityResourceBriefly Speciality { get; set; }
    }
}
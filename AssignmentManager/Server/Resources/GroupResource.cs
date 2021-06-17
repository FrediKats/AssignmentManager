using AssignmentManager.Server.Models;

namespace AssignmentManager.Server.Resources
{
    public class GroupResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SpecialityId { get; set; }
        public SpecialityResourceShort Speciality { get; set; }
    }
}
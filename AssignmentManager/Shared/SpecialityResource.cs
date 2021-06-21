using System.Collections.Generic;

namespace AssignmentManager.Shared
{
    public class SpecialityResource
    {
        public int Id { get; set; }
        public string StudyTypeName { get; set; }
        public EStudyType EnumStudyType { get; set; }
        public string Code { get; set; }
        public IList<GroupResourceBriefly> Groups { get; set; }
    }
}
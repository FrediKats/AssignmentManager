using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace AssignmentManager.Shared
{
    public class SpecialityResource
    {
        public int Id { get; set; }
        public string StudyTypeDescription { get; set; }
        public EStudyType StudyType { get; set; }
        public string Code { get; set; }
        public IList<GroupResourceBriefly> Groups { get; set; }
        public IList<SubjectResourceBriefly> Subjects { get; set; }
    }
}
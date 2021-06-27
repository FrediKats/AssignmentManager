using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace AssignmentManager.Shared
{
    public class SpecialityResourceBriefly
    {
        public int Id { get; set; }
        public string StudyTypeDescription { get; set; }
        public EStudyType StudyType { get; set; }
        public string Code { get; set; }
    }
}
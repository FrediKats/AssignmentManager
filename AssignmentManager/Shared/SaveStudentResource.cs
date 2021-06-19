using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Shared
{
    public class SaveStudentResource
    {
        [Required] public int IsuId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Lastname { get; set; }
        [Required] public string Email { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public int? GroupId { get; set; }
    }
}
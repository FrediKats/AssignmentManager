using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Shared
{
    public class SaveStudentResource
    {
        [Required] public int IsuId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Email { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        [Required] public int GroupId { get; set; }
    }
}
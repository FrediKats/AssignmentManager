using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Shared
{
    public class SaveSubjectResource
    {
        [Required]
        public string SubjectName { get; set; }
    }
}
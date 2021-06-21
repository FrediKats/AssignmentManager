using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Shared
{
    public class SaveInstructorSubjectResource
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int IsuId { get; set; }
        [Required]
        public int SubjectId { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Server.Resources
{
    public class SaveSubjectResource
    {
        [Required]
        public string SubjectName { get; set; }
    }
}
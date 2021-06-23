using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Shared
{
    public class SaveSolutionResource
    {
        [Required] public string Content { get; set; }
        [Required] public int? AssignmentId { get; set; }
        [Required] public int[] StudentsId { get; set; }
        public float? Grade { get; set; }
        public string Feedback { get; set; }
    }
}
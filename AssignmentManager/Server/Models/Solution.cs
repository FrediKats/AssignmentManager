using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Server.Models
{
    public class Solution
    {
        [Key] public int SolutionId { get; set; }
        [Required] public string Content { get; set; }
        public int? Grade { get; set; }
        public string Feedback { get; set; }
        
        [Required] public Assignment Assignment { get; set; }
        public virtual IList<Student> Students { get; set; }
    }
}
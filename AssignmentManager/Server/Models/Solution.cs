using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssignmentManager.Shared;

namespace AssignmentManager.Server.Models
{
    public class Solution
    {
        [Key] public int SolutionId { get; set; }
        [Required] public string Content { get; set; }
        public float? Grade { get; set; }
        public string Feedback { get; set; }
        
        public int AssignmentId { get; set; }
        [Required] public Assignment Assignment { get; set; }
        public virtual IList<Student> Students { get; set; }

        public Solution()
        {
            Students = new List<Student>();
        }

        public static implicit operator Solution(SaveSolutionResource solutionResource)
        {
            int asId = 0;
            if (solutionResource.AssignmentId.HasValue)
                asId = solutionResource.AssignmentId.Value;
            return new Solution
            {
                Content = solutionResource.Content,
                AssignmentId = asId,
                Grade = null,
                Feedback = String.Empty
            };
        }
        
    }
}
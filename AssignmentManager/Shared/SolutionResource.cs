using System.Collections.Generic;

namespace AssignmentManager.Shared
{
    public class SolutionResource
    {
        public int SolutionId { get; set; }
        public string Content { get; set; }
        public int? Grade { get; set; }
        public string Feedback { get; set; }
        
        public AssignmentResourceBriefly Assignment { get; set; }
        public IList<StudentResourceBriefly> Students { get; set; }
    }
}
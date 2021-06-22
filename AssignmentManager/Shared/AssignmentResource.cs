using System;
using System.Collections.Generic;

namespace AssignmentManager.Shared
{
    public class AssignmentResource
    {
        public int AssignmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        
        public SubjectResourceBriefly Subject { get; set; }
        public IList<SolutionResourceBriefly> Solutions { get; set; }
    }
}
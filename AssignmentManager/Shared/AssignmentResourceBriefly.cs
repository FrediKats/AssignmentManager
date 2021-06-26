using System;

namespace AssignmentManager.Shared
{
    public class AssignmentResourceBriefly
    {
        public int AssignmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        
        //[Required] public SubjectResourceBriefly Subject { get; set; }
    }
}
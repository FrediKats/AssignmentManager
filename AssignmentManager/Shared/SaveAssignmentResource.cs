using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Shared
{
    public class SaveAssignmentResource
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Deadline { get; set; }
        
        [Required]
        public int? SubjectId { get; set; }
    }
}
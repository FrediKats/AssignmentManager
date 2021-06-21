using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Server.Models
{
    public class Assignment
    {
        [Key] public int AssignmentId { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        [Required] public DateTime Deadline { get; set; }
        
        [Required] public Subject Subject { get; set; }
        public virtual IList<Solution> Solutions { get; set; }
    }
}
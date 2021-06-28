using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssignmentManager.Shared;
using AssignmentManager.Server.Extensions;

namespace AssignmentManager.Server.Models
{
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        
        
        public int SubjectId { get; set; }
        [Required]
        public Subject Subject { get; set; }
        
        public virtual IList<Solution> Solutions { get; set; }

        public Assignment()
        {
            Solutions = new List<Solution>();
        }
        
        public Assignment(SaveAssignmentResource assignmentResource)
        {
            Name = assignmentResource.Name;
            SubjectId = assignmentResource.SubjectId.Value;
            Description = assignmentResource.Description;
            Deadline = assignmentResource.Deadline.ParseToProjectTime();
        }
    }
}
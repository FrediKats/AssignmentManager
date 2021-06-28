using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Server.Models
{
    public class InstructorSubject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int IsuId { get; set; }
        [Required]
        public int SubjectId { get; set; }
    }
}
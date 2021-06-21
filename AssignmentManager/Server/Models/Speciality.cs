using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssignmentManager.Shared;

namespace AssignmentManager.Server.Models
{
    public class Speciality
    {
        [Key] public int Id { get; set; }
        [Required] public string Code { get; set; }
        [Required] public EStudyType EnumStudyType { get; set; }
        
        public virtual IList<Group> Groups { get; set; }
        public virtual IList<Subject> Subjects { get; set; }
    }
}
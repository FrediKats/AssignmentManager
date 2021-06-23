using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssignmentManager.Shared;
using Newtonsoft.Json;

namespace AssignmentManager.Server.Models
{
    public class Group
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        
        public int SpecialityId { get; set; }
        [Required] public Speciality Speciality { get; set; }
        public virtual IList<Student> Students { get; set; }

        public Group()
        {
            Students = new List<Student>();
        }

        public static implicit operator Group(SaveGroupResource groupResource)
        {
            return new Group()
            {
                Name = groupResource.Name,
                SpecialityId = groupResource.SpecialityId,
                Speciality = new Speciality(),
                Students = new List<Student>()
            };
        }
    }
}
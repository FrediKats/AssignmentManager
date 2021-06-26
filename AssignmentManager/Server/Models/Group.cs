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
            int asId = 0;
            if (groupResource.SpecialityId.HasValue)
                asId = groupResource.SpecialityId.Value;
            return new Group()
            {
                Name = groupResource.Name,
                SpecialityId = asId
            };
        }
    }
}
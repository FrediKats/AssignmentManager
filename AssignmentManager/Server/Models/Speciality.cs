using System;
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

        public Speciality()
        {
            Groups = new List<Group>();
            Subjects = new List<Subject>();
        }
        
        public static implicit operator Speciality(SaveSpecialityResource specialityResource)
        {
            EStudyType byteStudyType = EStudyType.Asp; 
            try
            {
                byteStudyType = (EStudyType) specialityResource.EnumStudyType;
            }
            catch (Exception)
            {
                throw new Exception("Can't cast enumStudyType to Enum");
            }
            return new Speciality()
            {
                Code = specialityResource.Code,
                EnumStudyType = byteStudyType,
                Groups = new List<Group>(),
                Subjects = new List<Subject>()
            };
        }
    }
}
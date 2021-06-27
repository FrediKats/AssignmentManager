using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssignmentManager.Server.Extensions;
using AssignmentManager.Shared;

namespace AssignmentManager.Server.Models
{
    public class Speciality
    {
        [Key] public int Id { get; set; }
        [Required] public string Code { get; set; }
        [Required] public EStudyType StudyType { get; set; }
        public virtual IList<Group> Groups { get; set; }
        public virtual IList<Subject> Subjects { get; set; }

        public Speciality()
        {
            Groups = new List<Group>();
            Subjects = new List<Subject>();
        }
        
        public Speciality(SaveSpecialityResource specialityResource)
        {
            if (!Enum.IsDefined(typeof(EStudyType), specialityResource.StudyType))
                throw new ArgumentException($"EnumerationValue must be of Enum type {string.Join(", ",  Enum.GetValues<EStudyType>())}");
            Code = specialityResource.Code;
            StudyType = specialityResource.StudyType;
            Groups = new List<Group>();
            Subjects = new List<Subject>();
        }
    }
}
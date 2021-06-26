using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace AssignmentManager.Server.Models
{
    public class Instructor
    {
        /*[Key] public int IsuId { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string FirstName { get; set; }
        public string PatronymicName { get; set; }
        [Required] public string Email { get; set; }
        public string Phone { get; set; }
 
        public virtual IList<Subject> Subjects { get; set; }*/

       public int IsuId { get; set; }
       public string LastName { get; set; }
       public string FirstName { get; set; }
       public string PatronymicName { get; set; }
       public string Email { get; set; }
       public string Phone { get; set; }

       public virtual IList<Subject> Subjects { get; set; }
    }
}
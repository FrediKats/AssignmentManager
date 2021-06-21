using System.Collections;
using System.Collections.Generic;

namespace AssignmentManager.Server.Models
{
    public class Instructor
    {
       public int IsuId { get; set; }
       public string LastName { get; set; }
       public string FirstName { get; set; }
       public string PatronymicName { get; set; }
       public string Email { get; set; }
       public string Phone { get; set; }

       //public IList<int> SubjectIds { get; set; } = new List<int>();
    }
}
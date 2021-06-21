using System.ComponentModel.DataAnnotations;

namespace AssignmentManager.Server.Resources
{
    public class SaveInstructorResource
    {
        [Required]
        public int IsuId { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string PatronymicName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
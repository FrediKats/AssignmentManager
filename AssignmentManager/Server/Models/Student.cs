namespace AssignmentManager.Server.Models
{
    public class Student
    {
        public int IsuId { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int? GroupId { get; set; }
        public Group Group { get; set; }
    }
}
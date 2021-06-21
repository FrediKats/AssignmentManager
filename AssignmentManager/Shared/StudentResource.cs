namespace AssignmentManager.Shared
{
    public class StudentResource
    {
        public int IsuId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public GroupResource Group { get; set; }
    }
}